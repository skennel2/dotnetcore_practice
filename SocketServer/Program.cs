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
            logger.Debug("df");
            new EchoServer().Start();
        }
    }
}
 