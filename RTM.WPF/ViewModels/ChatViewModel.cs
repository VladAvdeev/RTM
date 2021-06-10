using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using RTM.Common.Models;
using RTM.WPF.Clients.MainServer;
using RTM.WPF.Helpers;

namespace RTM.WPF.ViewModels
{
    public class ChatViewModel : NotifyPropertyChanged, IPageViewModel
    {
        private List<ChatGroup> chatGroups;

        public List<ChatGroup> ChatGroups
        {
            get => chatGroups;
            set => SetProperty(ref chatGroups, value);
        }

        private ChatGroup seletedChatGroup;
        public ChatGroup SelectedChatGroup
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
            RefreshChatCommand = new Command(RefreshChat);
            Load();
        }

        private void Send()
        {
            new ChatClient().SendMessage(SelectedChatGroup.Id, UserManager.User.Id, Message);
            Message = String.Empty;
            //RefreshChatAsync();
        }

        private async Task RefreshChatAsync()
        {
             await Task.Run(() => RefreshChatAuto());
        }
        private void RefreshChat()
        {
            SelectedChatGroup = new ChatClient().RefreshChatGroup(SelectedChatGroup.Id, UserManager.User.Id);
        }
        private void RefreshChatAuto()
        {
            while (true)
            {
                SelectedChatGroup = new ChatClient().RefreshChatGroup(SelectedChatGroup.Id, UserManager.User.Id);
                Thread.Sleep(3000);
            }
        }
        private void Load()
        {
            ChatGroups = new ChatClient().GetChats(UserManager.User.Id);
        }
    }
}
