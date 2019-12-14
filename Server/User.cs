using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class User
    {


        public string Name { get; private set; }

        public TcpClient Client { get; private set; }

        public NetworkStream ClientStream { get; private set; }

        private StreamWriter ClientWriter { get; set; }

        private StreamReader ClientReader { get; set; }

        public User(string name, TcpClient client)
        {
            this.Name = name;
            this.Client = client;
            this.ClientStream = client.GetStream();
            this.ClientWriter = new StreamWriter(ClientStream);
            this.ClientReader = new StreamReader(ClientStream);
        }

        public void Send(string message)
        {
            ClientWriter.Write(message);
            ClientWriter.Flush();
        }

        public string Receive()
        {
            return ClientReader.ReadLine();
        }

        public void Disconnect()
        {
            this.ClientStream.Close();
        }

    }
}
