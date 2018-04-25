using System;
using System.Text;
using NLog;

namespace SocketServer
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            new EchoServer().Start();
        }
    }
}
 