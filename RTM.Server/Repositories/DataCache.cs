using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTM.Server.Repositories
{
     static public class DataCache
    {
        static public UserRepository UserRepository { get; } = new UserRepository();
        static public ChatRepository ChatRepository { get; } = new ChatRepository();

        static public void Init()
        {
            UserRepository.Init();
            ChatRepository.Init();
        }

    }
}
