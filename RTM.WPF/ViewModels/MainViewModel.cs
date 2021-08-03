using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
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
        public ICommand SendCommand { get; }

        public string Message { get; set; }
        public MainViewModel()
        {
            WindowService = NinjectDi.Instanse.Get<IWindowService>();
            OpenProfileViewCommand = new Command(OpenProfileView);
            OpenChatViewCommand = new Command(OpenChatView);
            SendCommand = new Command(Send);
            ConnectHub();


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

        private async void Send()
        {
            try
            {
                await connection.InvokeAsync("SendAll", Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Message = String.Empty;
        }

        HubConnection connection;
        private async void ConnectHub()
        {
            connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:5001/notificationHub", options =>
                    {
                        //options.Headers.Add("Authorization", $"Bearer {UserManager.User.Token}");
                        //options.AccessTokenProvider = () => Task.FromResult(UserManager.User.Token);
                    })
                    .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<string>("ReceiveAll", (message) =>
            {
                MessageBox.Show($"{message}", "Оповещение всем", MessageBoxButton.OK, MessageBoxImage.Information);
            });

            try
            {
                await connection.StartAsync();
                MessageBox.Show("Connect");
            }
            catch (Exception ex)
            {
                MessageBox.Show("При страте коннекта произошло это:" + " " + ex.Message);
            }
        }



    }
}
