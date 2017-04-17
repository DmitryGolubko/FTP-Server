using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace FTP_Server
{
    class FTPServer : IDisposable
    {
        private const int COMMAND_PORT = 21;
        private TcpListener listener;
        private IPEndPoint localEndPoint;

        public FTPServer() : this(IPAddress.Any, COMMAND_PORT)
        {

        }

        public FTPServer(IPAddress ipAddress, int port)
        {
            localEndPoint = new IPEndPoint(ipAddress, port);
        }

        public void Start()
        {
            listener = new TcpListener(IPAddress.Any, COMMAND_PORT);
            listener.Start();
            listener.BeginAcceptTcpClient(HandleAcceptTcpClient, listener);
            
            string Host = System.Net.Dns.GetHostName();
            string IP = System.Net.Dns.GetHostByName(Host).AddressList[0].ToString();
            Console.WriteLine("FTP Server is started on IP: {0}", IP);
        }

        private void HandleAcceptTcpClient(IAsyncResult result)
        {
            listener.BeginAcceptTcpClient(HandleAcceptTcpClient, listener);
            TcpClient client = listener.EndAcceptTcpClient(result);
            var connection = new ClientConnection(client);
            ThreadPool.QueueUserWorkItem(connection.HandleClient, client);
        }

        public void Stop()
        {
            if (listener != null)
            {
                listener.Stop();
            }
        }

        public void Dispose()
        {
            Stop();
        }

    }
}
