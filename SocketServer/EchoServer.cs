using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    public class EchoServer
    {
        public void Start()
        {
            Console.WriteLine("Server Start");

            TcpListener listener = new TcpListener(IPAddress.Any, 7000);
            listener.Start();

            while (true)
            {
                using (TcpClient client = listener.AcceptTcpClient())
                {                
                    using (NetworkStream stream = client.GetStream())
                    {
                        var reader = new StreamReader(stream, Encoding.UTF8);

                        String sendValue = reader.ReadToEnd();
                        Console.WriteLine(sendValue);
                    }
                }
            }
        }
    }
}
