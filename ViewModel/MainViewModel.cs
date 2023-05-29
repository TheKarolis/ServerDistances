using ServerDistances.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDistances.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            client = new ClientLogic.Client();
            _authenticationViewModel = new AuthenticationViewModel(client);
            _listControlViewModel = new ListControlViewModel(client);
        }

        ClientLogic.Client client;

        private AuthenticationViewModel _authenticationViewModel;
        private ListControlViewModel _listControlViewModel;

        public AuthenticationViewModel AuthenticationViewModel { get { return _authenticationViewModel; } }
        public ListControlViewModel ListControlViewModel { get { return _listControlViewModel; } }
    }
}
