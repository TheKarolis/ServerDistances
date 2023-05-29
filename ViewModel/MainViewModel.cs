using ServerDistances.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerDistances.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            client = new ClientLogic.Client();
            _authenticationViewModel = new AuthenticationViewModel(ref client);
            _listControlViewModel = new ListControlViewModel(ref client);
        }

        ClientLogic.Client client;

        private AuthenticationViewModel _authenticationViewModel;
        private ListControlViewModel _listControlViewModel;

        public AuthenticationViewModel AuthenticationViewModel { get { return _authenticationViewModel; } }
        public ListControlViewModel ListControlViewModel { get { return _listControlViewModel; } }
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged = delegate { };
    }
}
