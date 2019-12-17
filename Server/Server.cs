using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {

        /// <summary>
        /// An object used to manage connected users.
        /// </summary>
        public UserManager UserManager { get; private set; }
                
        /// <summary>
        /// Current instance of the server.
        /// </summary>
        public static Server Instance { get; private set; }

        /// <summary>
        /// A listener that accepts incoming connections
        /// </summary>
        private TcpListener Listener;

        /// <summary>
        /// Constructor of Server
        /// </summary>
        /// <param name="address">The address of the server</param>
        /// <param name="port">The server's port</param>
        public Server(IPAddress address, int port)
        {
            Server.Instance = this;
            this.UserManager = new UserManager();
            this.Listener = new TcpListener(address, port);
        }

        /// <summary>
        /// Accept a connection
        /// </summary>
        /// <param name="obj">An object. Should be the client</param>
        public void AcceptConnections(object obj)
        {
            TcpClient client = (TcpClient) obj;
            User clientUser = new User(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), client);
            UserManager.Register(clientUser);
        }

        /// <summary>
        /// Starts the server
        /// </summary>
        public void Start()
        {
            Listener.Start();
            Console.WriteLine("Started server...");
            while (true)
            {
                // Accept an incoming connection and add it to the thread queue
                TcpClient client = Listener.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(AcceptConnections, client);
            }
        }

        /// <summary>
        /// Stops the server
        /// </summary>
        public void Stop()
        {
            Listener.Stop();
            Console.WriteLine("Stopped server...");
        }

    }
}
