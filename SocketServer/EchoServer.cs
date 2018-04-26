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
            var listener = new TcpListener(IPAddress.Any, 7000);
            listener.Start();

            isRunning = true;

            while (isRunning)
            {
                using (var client = listener.AcceptTcpClient())
                using (var stream = client.GetStream())
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                using (var writer = new StreamWriter(stream, Encoding.UTF8))    
                {

                    var sendValue = reader.ReadToEnd();
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
