using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Server
{
    class RNFRcommand : Command
    {
        public override string Execute(string arguments, ClientConnection connection)
        {
            connection.renameFrom = arguments;
            return "350 Requested file action pending further information";
        }
    }
}
