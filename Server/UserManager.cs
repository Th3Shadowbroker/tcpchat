using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// A class used to manager users
    /// </summary>
    class UserManager
    {

        /// <summary>
        /// The list containing all managed users
        /// </summary>
        private List<User> users = new List<User>();

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user">The user</param>
        public void Register(User user)
        {
            users.Add(user);
        }

        /// <summary>
        /// Remove a registered user
        /// </summary>
        /// <param name="user">The user</param>
        public void Remove(User user)
        {
            users.Remove(user);
        }

        /// <summary>
        /// Get a user by it their IP address
        /// </summary>
        /// <param name="address">The address</param>
        /// <returns>The user</returns>
        public User GetByAddress(IPAddress address)
        {
            return users.FirstOrDefault(usr => ((IPEndPoint)usr.Client.Client.RemoteEndPoint).Address == address);
        }

        /// <summary>
        /// Get a user by name
        /// </summary>
        /// <param name="name">The users name</param>
        /// <returns>User</returns>
        public User GetByName(string name)
        {
            return users.FirstOrDefault(usr => usr.Name == name);
        }

        /// <summary>
        /// Send a broadcast message to all clients
        /// </summary>
        /// <param name="message">The message</param>
        public void Broadcast(string message)
        {
            users.ForEach(usr =>
            {
                usr.Send("Server: " + message);
            });
        }

        /// <summary>
        /// Send a message
        /// </summary>
        /// <param name="sender">The sender of the message</param>
        /// <param name="message">The message</param>
        public void Send(User sender, string message)
        {
            users.FindAll(usr => usr != sender).ForEach(usr =>
            {
                usr.Send(sender.Name + ": " + message);
            });
        }

    }
}
