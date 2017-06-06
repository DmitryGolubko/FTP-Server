using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Server
{
    class QUITcommand : Command
    {
        public override string Execute(string arguments, ClientConnection connection)
        {
            return "221 Service closing control connection";
        }
    }
}
