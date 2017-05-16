using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Server
{
    class USERcommand : Command
    {
        public override string Execute(string arguments, ClientConnection connection)
        {
            connection.connectedClientusername = arguments;
            return "331 USER NAME OK, NEED PASSWORD";
        }
    }
}
