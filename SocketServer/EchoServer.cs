using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    public class EchoServer
    {
        private bool isRunning;

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 7000);
            listener.Start();

            isRunning = true;

            while (isRunning)
            {
                using (TcpClient client = listener.AcceptTcpClient())
                using (NetworkStream stream = client.GetStream())
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                using (var writer = new StreamWriter(stream, Encoding.UTF8))    
                {

                    String sendValue = reader.ReadToEnd();
                    writer.WriteLine(sendValue);

                    Console.WriteLine(sendValue);
                }
            }
        }

        public void Stop()
        {
            isRunning = false;    
        }

        private void LogMessage(String msg)
        {
            Console.WriteLine(msg);
        }
    }
}
