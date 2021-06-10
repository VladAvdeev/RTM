using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ninject;
using RTM.WPF.Clients.Other;
using RTM.WPF.Helpers;
using RTM.WPF.Services;

namespace RTM.WPF.ViewModels
{
    class MainViewModel : NotifyPropertyChanged, IPageViewModel
    {
        private IPageViewModel activeView;
        public IPageViewModel ActiveView
        {
            get => activeView;
            set => SetProperty(ref activeView, value);
        }

        public IWindowService WindowService { get; }
        public ICommand OpenProfileViewCommand { get; }
        public ICommand OpenChatViewCommand { get; }
        public MainViewModel()
        {
            WindowService = NinjectDi.Instanse.Get<IWindowService>();
            OpenProfileViewCommand = new Command(OpenProfileView);
            OpenChatViewCommand = new Command(OpenChatView);
            
        }

        //Сделать отдельный сервис по работе с views
        private void OpenProfileView()
        {
            ActiveView = new ProfileViewModel();
        }

        private void OpenChatView()
        {
            ActiveView = new ChatViewModel();
        }



    }
}
