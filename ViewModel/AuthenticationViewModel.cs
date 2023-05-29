﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerDistances.ViewModel
{
    class AuthenticationViewModel : BaseViewModel
    {
        ClientLogic.Client client = new ClientLogic.Client();
        public AuthenticationViewModel(ClientLogic.Client client)
        {
            client = new ClientLogic.Client();
            this.client = client;
        }
        public AuthenticationViewModel() { }

        private string failureLabel = "Not authenticated";

        public string FailureLabel { get => failureLabel; 
            set
            {
                if(failureLabel == value) return;
                failureLabel = value;
                OnPropertyChanged(nameof(FailureLabel));
            }
        }

        /// <summary>
        /// Method for authentication of a user to access server data on web
        /// </summary>
        public void login(string name, string pass)
        {
            Task<bool> loginTask = Task.Run(() => client.Authenticate(name, pass));
            loginTask.Wait();
            FailureLabel = loginTask.Result ? FailureLabel = "Authenticated" : "Wrong name or password";

            log.Info("Authentication attempt result: " + FailureLabel);
        }

        public string Name { get; set; }
    }
}
