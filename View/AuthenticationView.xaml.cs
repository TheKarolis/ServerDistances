using ServerDistances.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerDistances.View
{
    /// <summary>
    /// Interaction logic for AuthenticationView.xaml
    /// </summary>
    public partial class AuthenticationView : UserControl
    {
        public AuthenticationView()
        {
            InitializeComponent();
            vm = new AuthenticationViewModel();
            vm = (AuthenticationViewModel)DataContext;
        }

        AuthenticationViewModel vm;

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            vm = (AuthenticationViewModel)DataContext;
            vm.login(textBox.Text, passwordBox.Password);
        }
    }
}
