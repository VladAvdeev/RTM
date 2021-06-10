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
    public class ProfileViewModel : NotifyPropertyChanged, IPageViewModel
    {
        private UserClient userClient = new UserClient();
        private AuthClient authClient = new AuthClient();
        private User user;
        public User User 
        {
            get => UserManager.User;
        }
       
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public ICommand UpdateCommand { get; }
        public ICommand RefreshCommand { get; }
        public ProfileViewModel()
        {
            UpdateCommand = new Command(UpdateName, () => Name != null && Name.Length != 0 || Name != UserManager.User.Name);
            Name = User.Name;
        }
        private void UpdateName()
        {
            userClient.RenameUser(UserManager.User.Id, Name);
            User updatedUser = authClient.AuthByName(Name);
            if(updatedUser != null)
            {
                UserManager.User = updatedUser;
                MessageBox.Show("Имя пользователя обновлено");
            }

        }
        
    }
}
