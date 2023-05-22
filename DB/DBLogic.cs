using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDistances.DB
{
    class DBLogic
    {
        public DBLogic()
        {
        }
        private static readonly log4net.ILog log = log4net.LogManager
            .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string queryInsertIntoServerTb = "INSERT OR REPLACE INTO ServerTb (Name, Distance) VALUES (@Name, @Distance)";
        private const string querySelectServerTbAll = "SELECT * FROM ServerTb ORDER BY Distance DESC";
        public void AddListOfServers(List<Model.Server> servers)
        {
            if(servers != null)
            foreach (Model.Server srv in servers)
            {
                Dictionary<string, object> dct = new Dictionary<string, object>();
                dct.Add("Name", srv.name);
                dct.Add("Distance", srv.distance);
                WriteQr(queryInsertIntoServerTb, dct);
            }
            log.Info("Servers added to DB");
        }

        public List<Model.Server> GetListOfServers()
        {
            List<Model.Server> servers = new List<Model.Server>();
            servers = ReadServersQr(querySelectServerTbAll);
            return servers;
        }

        /// <summary>
        /// Method for writing a server object into a DB
        /// </summary>
        /// <param name="query"></param>
        /// <param name="args">record to insert into DB</param>
        /// <returns>Success of the query</returns>
        private bool WriteQr(string query, Dictionary<string, object> args)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source = DB\\ServersDB.db");
            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);

                foreach (var obj in args)
                {
                    command.Parameters.AddWithValue(obj.Key, obj.Value);
                }

                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Failed to add a server to a DB: " + ex.Message);
            }

            connection.Close();
            return false;            
        }

        /// <summary>
        /// Reading servers data from DB
        /// </summary>
        /// <returns>List of servers</returns>
        private List<Model.Server> ReadServersQr(string query) 
        {
            List<Model.Server> servers = new List<Model.Server>();
            SQLiteConnection connection = new SQLiteConnection("Data Source = DB\\ServersDB.db");
            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    servers.Add(new Model.Server(reader.GetString(1), reader.GetInt32(2)));
                }
                
                connection.Close();
                log.Info("Server info retrieved from DB");
                return servers;
            }
            catch (Exception ex)
            {
                log.Error("Failed to retrieve server info from DB: " + ex.Message);
            }

            connection.Close();
            return null;
        }

    }
}
