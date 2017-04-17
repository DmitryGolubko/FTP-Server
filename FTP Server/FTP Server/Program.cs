using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace FTP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FTPServer server = new FTPServer(IPAddress.Any, 21))
            {
                server.Start();

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);
            }
        }
    }
}
