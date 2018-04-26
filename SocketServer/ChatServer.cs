using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SocketServer
{
    public class ChatServer
    {
        private TcpListener server;

        private bool isRunning;

        private object lockObject = new object();

        public ChatServer()
        {
            isRunning = false;

            var port = 8181;
            var ipAddress = IPAddress.Parse("127.0.0.1");

            server = new TcpListener(ipAddress, port);
        }

        public void Run()
        {
            isRunning = true;

            server.Start();

            var t1 = Task.Factory.StartNew(Listen);

            t1.Wait();
        }

        public void Listen()
        {
            while (isRunning)
            {
                lock (lockObject)
                {
                    var client = server.AcceptTcpClient();
                    if(client != null)
                    {
                        Console.WriteLine("Accepted");

                        var stream = client.GetStream();
                    }
                }
            }
        }
    }
}