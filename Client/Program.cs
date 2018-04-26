using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestEchoServer();
        }

        public static void RequestEchoServer()
        {
            TcpClient client = new TcpClient();

            client.Connect(IPAddress.Loopback, 8181);

            using (NetworkStream stream = client.GetStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes("hello world");

                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
