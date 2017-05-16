using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace FTP_Server
{
    class PASVcommand : Command
    {
        public override string Execute(string arguments, ClientConnection connection)
        {
            connection.dataConnectionType = ClientConnection.DataConnectionType.Passive;

            IPAddress localIp = ((IPEndPoint)connection.connectedControlClient.Client.LocalEndPoint).Address;

            connection.passiveListener = new TcpListener(localIp, 0);
            connection.passiveListener.Start();

            IPEndPoint passiveListenerEndpoint = (IPEndPoint)connection.passiveListener.LocalEndpoint;

            byte[] address = passiveListenerEndpoint.Address.GetAddressBytes();
            short port = (short)passiveListenerEndpoint.Port;

            byte[] portArray = BitConverter.GetBytes(port);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(portArray);

            return string.Format("227 Entering Passive Mode ({0},{1},{2},{3},{4},{5})", address[0], address[1], address[2], address[3], portArray[0], portArray[1]);
        }
    }
}
