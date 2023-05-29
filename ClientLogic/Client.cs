using ServerDistances.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace ServerDistances.ClientLogic
{
    /// <summary>
    /// Class for interactions with a web page
    /// </summary>
    class Client
    {
        private static readonly log4net.ILog log = log4net.LogManager
            .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string name = "";
        private static string token = "";
        private string serversUrl = "https://playground.tesonet.lt/v1/servers";
        private string tokenUrl = "https://playground.tesonet.lt/v1/tokens";

        public static bool isAuthentified { get; private set; } = false;

        HttpClient client = new HttpClient();

        public Client() { }

        class Token
        {
            public string token { get; set;}
        }

        /// <summary>
        /// Receiving a token via Post method using a name and a password
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns>Whether authentication is successful</returns>
        public async Task<bool> Authenticate(string name, string pass)
        {
            
            Dictionary<string, string> postData = new Dictionary<string, string>
            {
                { "username", name },
                { "password", pass }
            };
            try
            {
                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
                var response = await client.PostAsync("https://playground.tesonet.lt/v1/tokens", content);
                response.EnsureSuccessStatusCode();
                string responseString = await response.Content.ReadAsStringAsync();
                int indexofleft = responseString.IndexOf('<');
                int indexofright = responseString.IndexOf('>');

                token = JsonSerializer.Deserialize<Token>(responseString).token;
                isAuthentified = true;
                log.Info("Authentication successful");
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Authentication not successful: " + ex.Message.ToString());
                isAuthentified = false;
                return false;
            }
        }

        /// <summary>
        /// Method for getting servers info from web page
        /// </summary>
        /// <returns></returns>
        public async Task <List<Model.Server>> GetServersAsync()
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseString = await client.GetStringAsync("https://playground.tesonet.lt/v1/servers");
                responseString = responseString.Trim();
                return GetResponseDict(responseString);
            }
            catch (Exception ex)
            {
                log.Error("Data from web retrieval failed: " + ex.Message.ToString());
            }
            
            return null;
        }
        /// <summary>
        /// Method for creating a list from json
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns>list of objects</returns>
        private List<Model.Server> GetResponseDict(string jsonData)
        {
            List<Model.Server> responseList = new List<Model.Server>();
            try
            {
                responseList = JsonSerializer.Deserialize<List<Model.Server>>(jsonData);
            }
            catch (Exception ex)
            {
                log.Error("Data conversion failure: " + ex.Message.ToString());
            }
            log.Info("Servers list retrieved from web. Server count: " + responseList.Count);
            return responseList;
        }
    }
}
