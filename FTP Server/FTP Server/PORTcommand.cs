using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace FTP_Server
{
    class PORTcommand : Command
    {
        public override string Execute(string hostPort, ClientConnection connection)
        {
            connection.dataConnectionType = ClientConnection.DataConnectionType.Active;

            string[] ipAndPort = hostPort.Split(',');

            byte[] ipAddress = new byte[4];
            byte[] port = new byte[2];

            for (int i = 0; i < 4; i++)
            {
                ipAddress[i] = Convert.ToByte(ipAndPort[i]);
            }

            for (int i = 4; i < 6; i++)
            {
                port[i - 4] = Convert.ToByte(ipAndPort[i]);
            }

            if (BitConverter.IsLittleEndian)
                Array.Reverse(port);
            int intport = BitConverter.ToUInt16(port, 0);
            connection.dataEndpoint = new IPEndPoint(new IPAddress(ipAddress), BitConverter.ToUInt16(port, 0));

            return "200 Data Connection Established";
        }
    }
}
