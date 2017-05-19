using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Server
{
    class STORcommand : Command
    {
        public override string Execute(string pathname, ClientConnection connection)
        {
            pathname = connection.NormalizeFilename(pathname);

            if (pathname != null)
            {
                var state = new ClientConnection.DataConnectionOperation { Arguments = pathname, Operation = connection.StoreOperation };

                connection.SetupDataConnectionOperation(state);

                return string.Format("150 Opening {0} mode data transfer for STOR", connection.dataConnectionType);
            }
            return "450 Requested file action not taken";
        }
    }
}
