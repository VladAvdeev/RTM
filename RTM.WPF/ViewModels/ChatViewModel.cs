using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using RTM.Common.Models;
using RTM.WPF.Clients.MainServer;
using RTM.WPF.Helpers;
using RTM.WPF.Models;
using UserClient = RTM.WPF.Models.UserClient;

namespace RTM.WPF.ViewModels
{
    public class ChatViewModel : NotifyPropertyChanged, IPageViewModel
    {
        HubConnection connection;

        private ObservableCollection<ChatGroupClient> chatGroups;

        public ObservableCollection<ChatGroupClient> ChatGroups
        {
            get => chatGroups;
            set => SetProperty(ref chatGroups, value);
        }

        private ChatGroupClient seletedChatGroup;
        public ChatGroupClient SelectedChatGroup
        {
            get => seletedChatGroup;
            set
            {
                SetProperty(ref seletedChatGroup, value);
            }
        }

        private string message;
        public string Message { get => message; set => SetProperty(ref message, value); }

        public ICommand SendCommand { get; }
        public ICommand RefreshChatCommand { get; }
        public ChatViewModel()
        {
            SendCommand = new Command(Send, () => Message != null && Message.Length != 0);
           // RefreshChatCommand = new Command(RefreshChat);
            Load();
        }

        private async void Send()
        {
            //new ChatClient().SendMessage(SelectedChatGroup.Id, UserManager.User.Id, Message);
            try
            {
                await connection.InvokeAsync("SendMessage",
                    SelectedChatGroup.Id, UserManager.User, Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Message = String.Empty;
            //connection.DisposeAsync();
            //RefreshChatAsync();
        }

        private void RefreshOnline(List<int> ids)
        {
            foreach (var chat in ChatGroups)
            {
                foreach (var user in chat.Users)
                {
                    if (ids.Contains(user.Id))
                        user.IsOnline = true;
                    else
                        user.IsOnline = false;
                }
            }
            ChatGroups = new ObservableCollection<ChatGroupClient>(ChatGroups);
        }
        private async void Load()
        {
            ChatGroups = new ChatClient().GetChats(UserManager.User.Id);

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/chatHub", options =>
                {
                    options.HttpMessageHandlerFactory = message =>
                    {
                        if (message is HttpClientHandler clientHandler)
                            clientHandler.ServerCertificateCustomValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    };
                })
                .WithAutomaticReconnect()
                .Build();

            //connection.Closed += async (error) =>
            //{
            //    await Task.Delay(new Random().Next(0, 5) * 1000);
            //    await connection.StartAsync();
            //};

            connection.On<int, User, string>("ReceiveMessage", (chatId, user, message) =>
            {
               var chat = ChatGroups.Where(x => x.Id == chatId).FirstOrDefault();
               chat.AddMessage(new MessageClient() { Id = 0, User = new UserClient() { Id = user.Id, Name = user.Name }, Content = message, Date = DateTime.Now });
               ChatGroups = new ObservableCollection<ChatGroupClient>(ChatGroups.OrderByDescending(x => x.LastMessage.Date).ToList());
            });

            connection.On("SendUser", () => connection.InvokeAsync("SendUser", UserManager.User.Id));

            connection.On<List<int>>("ReshreshOnline", (usersIds) => RefreshOnline(usersIds));

            try
            {
                await connection.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("При страте коннекта произошло это:" + " " + ex.Message);
            }
            foreach (var group in ChatGroups)
            {
                try
                {
                    await connection.InvokeAsync("EnterGroup", group.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("При коннекте к группе произошло это:" + " " + ex.Message);
                }
            }
        }
    }
}
