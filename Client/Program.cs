using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();

            client.Connect(IPAddress.Loopback, 7000);

            using(NetworkStream stream = client.GetStream())
            {
                byte[] bytes = Encoding.UTF8.GetBytes("hello world");

                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
