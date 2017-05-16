using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FTP_Server
{
    class RETRcommand : Command
    {
        public override string Execute(string pathname, ClientConnection connection)
        {
            pathname = connection.NormalizeFilename(pathname);

            if (pathname != null)
            {
                if (File.Exists(pathname))
                {
                    var state = new ClientConnection.DataConnectionOperation { Arguments = pathname, Operation = connection.RetrieveOperation };

                    connection.SetupDataConnectionOperation(state);

                    return string.Format("150 Opening {0} mode data transfer for RETR", connection.dataConnectionType);
                }
            }

            return "550 File Not Found";
        }
    }
}
