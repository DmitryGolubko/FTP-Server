using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Server
{
    class SYSTcommand : Command
    {
        public override string Execute(string arguments, ClientConnection connection)
        {
            return "215 WINDOWS-NT-10";
        }
    }
}
