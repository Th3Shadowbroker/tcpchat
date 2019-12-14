using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class UserManager
    {

        private List<User> users = new List<User>();

        public void Register(User user)
        {
            users.Add(user);
        }

        public void Remove(User user)
        {
            users.Remove(user);
        }

        public User GetByAddress(IPAddress address)
        {
            return users.FirstOrDefault(usr => ((IPEndPoint)usr.Client.Client.RemoteEndPoint).Address == address);
        }

        public User GetByName(string name)
        {
            return users.FirstOrDefault(usr => usr.Name == name);
        }

        public void Broadcast(string message)
        {
            users.ForEach(usr =>
            {
                usr.Send("Server: " + message);
            });
        }

        public void Send(User sender, string message)
        {
            sender.Send(sender.Name + ": " + message);
        }

    }
}
