using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Ninject;
using RTM.Common.Models;
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

        private ObservableCollection<NotificationModel> notifications;
        public ObservableCollection<NotificationModel> Notifications { get => notifications; set => SetProperty(ref notifications, value); }
        private Visibility notificationVisibility;
        public Visibility NotificationVisibility { get => notificationVisibility; set => SetProperty(ref notificationVisibility, value); }

        public MainViewModel()
        {
            WindowService = NinjectDi.Instanse.Get<IWindowService>();
            OpenProfileViewCommand = new Command(OpenProfileView);
            OpenChatViewCommand = new Command(OpenChatView);
            Notifications = new ObservableCollection<NotificationModel>();
            NotificationVisibility = Visibility.Collapsed;
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

        private async void AddNotification(NotificationModel notification)
        {
            Notifications.Add(notification);
            NotificationVisibility = Visibility.Visible;
            await Task.Delay(10 * 1000);
            Notifications.Remove(notification);
            if(Notifications.Count == 0)
                NotificationVisibility = Visibility.Collapsed;
        }

        HubConnection connection;
        private async void ConnectHub()
        {
            connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:5001/notificationHub", options =>
                    {
                        //options.Headers.Add("Authorization", $"Bearer {UserManager.User.Token}");
                        //options.AccessTokenProvider = () => Task.FromResult(UserManager.User.Token);
                        options.HttpMessageHandlerFactory = message =>
                        {
                            if (message is HttpClientHandler clientHandler)
                                clientHandler.ServerCertificateCustomValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };
                            return message;
                        };
                    })
                    .WithAutomaticReconnect()
                    .Build();
            //connection.Headers.Add("token", UserManager.User.Token);

            //connection.Closed += async (error) =>
            //{
            //    await Task.Delay(new Random().Next(0, 5) * 1000);
            //    await connection.StartAsync();
            //};

            connection.On<NotificationModel>("ReceiveAll", (notification) =>
            {
                AddNotification(notification);
            });

            connection.On<NotificationModel>("Receive", (notification) =>
            {
                AddNotification(notification);
            });

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("Connect", UserManager.User.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("При страте коннекта произошло это:" + " " + ex.Message);
            }
        }




    }
}
