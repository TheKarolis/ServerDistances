using ServerDistances.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServerDistances.ViewModel
{
    class ListControlViewModel : BaseViewModel
    {
        public ListControlViewModel(ClientLogic.Client client)
        {
            this.client = new ClientLogic.Client();
            this.client = client;
        }

        public ListControlViewModel() { }

        ClientLogic.Client client;
        List<Model.Server> servers = new List<Model.Server>();
        DBLogic dbLogic = new DBLogic();
        public ObservableCollection<Model.Server> ServersObservableList { get; set; }
            = new ObservableCollection<Model.Server>();

        public async void getServersFromWeb()
        {
            if (ClientLogic.Client.isAuthentified)
            {
                servers = new List<Model.Server>();
                servers = await client.GetServersAsync();

                dbLogic.AddListOfServers(servers);
                MessageBox.Show("Data added to database");

            }
            else
            {
                MessageBox.Show("Not authenticated");
            }
        }

        public void getServersFromDB()
        {
            servers = new List<Model.Server>();
            servers = dbLogic.GetListOfServers();
            ServersObservableList.Clear();
            ServersObservableList = new ObservableCollection<Model.Server>(servers);
            OnPropertyChanged(nameof(ServersObservableList));
        }
    }
}
