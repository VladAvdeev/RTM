using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RTM.Common.Models;

namespace RTM.Server.Repositories
{
    public class UserRepository
    {
        public List<User> Users { get; private set; }
        private int sequence = 0;
        public void Init()
        {
            Users = new List<User>
            {
                new User(0, "Admin"),
                new User(++sequence, "Дима"),
                new User(++sequence, "Саша"),
                new User(++sequence, "Влад"),
                new User(++sequence, "Сларк")
            };
        }
        public User GetUserByName(string name)
        {
            return Users.Where(x => x.Name == name).FirstOrDefault();
        }
        public void UserRename(int id, string name)
        {
            User user = Users.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
                throw new Exception("Пользователя не существует");
            user.Name = name;
        }
        public void AddUser(string name)
        {
            Users.Add(new User(++sequence, name));
        }

        public User GetAdmin()
        {
            return Users.Where(x => x.Id == 0).FirstOrDefault();
        }

        private int GetSequence()
        {
            return ++sequence;
        }
    }
}
