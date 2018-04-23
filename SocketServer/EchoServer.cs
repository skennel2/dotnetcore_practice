using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    public class EchoServer
    {
        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 7000);
            listener.Start();

            byte[] buff = new byte[1024];

            while (true)
            {
                using (TcpClient client = listener.AcceptTcpClient())
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        int byteSize = stream.Read(buff, 0, buff.Length);

                        while (byteSize > 0)
                        {
                            stream.Write(buff, 0, byteSize);
                        }
                    }
                }
            }
        }
    }
}
