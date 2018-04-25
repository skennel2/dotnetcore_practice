using System;
using System.Text;
using NLog;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            new EchoServer().Start();
        }
    }
}
 