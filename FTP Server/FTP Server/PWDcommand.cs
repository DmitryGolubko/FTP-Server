using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Server
{
    class PWDcommand : Command
    {
        public override string Execute(string arguments, ClientConnection connection)
        {
            string current = connection.currentDirectory.Replace(connection.root, string.Empty).Replace('\\', '/');
        
            if (current.Length == 0)
            {
                current = "/";
            }
            return string.Format("257 \"{0}\" is current directory.", current); ;
        }
    }
}
