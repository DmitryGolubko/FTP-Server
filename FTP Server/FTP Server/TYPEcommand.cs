using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Server
{
    class TYPEcommand : Command
    {
        public override string Execute(string arguments, ClientConnection connection)
        {
            switch (arguments.ToUpperInvariant())
            {
                case "A":
                    connection.connectionType = ClientConnection.TransferType.Ascii;
                    break;
                case "I":
                    connection.connectionType = ClientConnection.TransferType.Image;
                    break;
                default:
                    return "504 Command not implemented for that parameter";
            }
            return string.Format("200 Type set to {0}", connection.connectionType);
        }
    }
}
