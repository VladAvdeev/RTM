using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RTM.WPF.Models
{
    public class ChatGroupClient
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("users")]
        public List<UserClient> Users { get; set; }
        [JsonPropertyName("messages")]
        public ObservableCollection<MessageClient> Messages { get; private set; }
        public MessageClient LastMessage { get => Messages?.Where(x => x.Date == Messages.Max(y => y.Date)).FirstOrDefault(); private set { } }

        public void AddMessage(MessageClient message)
        {
            if (Messages == null)
                Messages = new ObservableCollection<MessageClient>();
            Messages.Add(message);
            LastMessage = Messages.Where(x => x.Date == Messages.Max(y => y.Date)).FirstOrDefault();
        }
    }
}
