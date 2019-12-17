using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class User
    {

        /// <summary>
        /// The client's name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The actual client
        /// </summary>
        public TcpClient Client { get; private set; }

        /// <summary>
        /// The stream between client and server
        /// </summary>
        public NetworkStream ClientStream { get; private set; }

        /// <summary>
        /// Writer for the client/server stream
        /// </summary>
        private StreamWriter ClientWriter { get; set; }

        /// <summary>
        /// Reader for the client/server stream
        /// </summary>
        private StreamReader ClientReader { get; set; }

        /// <summary>
        /// A thread that takes care of incoming data
        /// </summary>
        private Thread UserThread;

        /// <summary>
        /// Constructor of User
        /// </summary>
        /// <param name="name">The users name</param>
        /// <param name="client">The client</param>
        public User(string name, TcpClient client)
        {
            this.Name = name;
            this.Client = client;
            this.ClientStream = client.GetStream();
            this.ClientWriter = new StreamWriter(ClientStream);
            this.ClientReader = new StreamReader(ClientStream);
            this.UserThread = new Thread(() => {
                Server.Instance.UserManager.Broadcast(this.Name + " joined");
                while(true)
                {
                    string message = Receive();
                    Console.WriteLine("{0}: {1}", Name, message);
                    Server.Instance.UserManager.Send(this, message);
                }
            });
            this.UserThread.Start();
        }

        /// <summary>
        /// Send a message to the client
        /// </summary>
        /// <param name="message">The message</param>
        public void Send(string message)
        {
            ClientWriter.WriteLine(message);
            ClientWriter.Flush();
            Console.WriteLine(Name + ": " + message);
        }

        /// <summary>
        /// Receive an incoming message
        /// </summary>
        /// <returns>The message</returns>
        public string Receive()
        {
            return ClientReader.ReadLine();
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            this.ClientWriter.Close();
            this.ClientReader.Close();
            this.ClientStream.Close();
        }

    }
}
