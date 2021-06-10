using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTM.Common.Models;
using RTM.WPF.Helpers;

namespace RTM.WPF.ViewModels
{
    public class ProfileViewModel : NotifyPropertyChanged, IPageViewModel
    {
        private User user;
        public User User 
        {
            get => UserManager.User;
        }
    }
}
