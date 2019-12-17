using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private TcpClient Client;

        private NetworkStream ClientStream;

        private StreamWriter ClientWriter;

        private StreamReader ClientReader;

        private Thread ClientThread;

        public MainWindow()
        {
            InitializeComponent();
            this.Client = new TcpClient();
            this.Client.Connect(IPAddress.Parse("127.0.0.1"), 3305);
            this.ClientStream = Client.GetStream();
            this.ClientWriter = new StreamWriter(ClientStream);
            this.ClientReader = new StreamReader(ClientStream);
            this.ClientThread = new Thread(() => {
                while(true)
                {
                    string message = ClientReader.ReadLine();
                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ChatHistory.AppendText(message + "\n");
                    }));
                }
            });
            this.ClientThread.Start();
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            ChatHistory.AppendText("You: " + Message.Text + "\n");
            ClientWriter.WriteLine(Message.Text);
            ClientWriter.Flush();
        }
    }
}
