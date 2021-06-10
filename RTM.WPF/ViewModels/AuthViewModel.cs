using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RTM.Common.Models;
using RTM.WPF.Clients.MainServer;
using RTM.WPF.Helpers;

namespace RTM.WPF.ViewModels
{
    class AuthViewModel : NotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public ICommand AuthCommand { get; }
        public ICommand RegisterCommand { get; }
        public AuthViewModel()
        {
            AuthCommand = new Command(Auth, () =>Name != null && Name.Length != 0);
        }

        private void Auth()
        {
            User user = new AuthClient().AuthByName(Name);
            if (user != null)
            {
                UserManager.User = user;
                MainWindow window = new MainWindow();
                Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();
                window.Show();
            }
        }
        
    }
}
