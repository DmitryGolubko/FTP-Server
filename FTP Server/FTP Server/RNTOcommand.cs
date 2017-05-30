using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FTP_Server
{
    class RNTOcommand : Command
    {
        public override string Execute(string renameTo, ClientConnection connection)
        {
            if (string.IsNullOrWhiteSpace(connection.renameFrom) || string.IsNullOrWhiteSpace(renameTo))
            {
                return "450 Requested file action not taken";
            }

            connection.renameFrom = connection.NormalizeFilename(connection.renameFrom);
            renameTo = connection.NormalizeFilename(renameTo);

            if (connection.renameFrom != null && renameTo != null)
            {
                if (File.Exists(connection.renameFrom))
                {
                    File.Move(connection.renameFrom, renameTo);
                }
                else if (Directory.Exists(connection.renameFrom))
                {
                    Directory.Move(connection.renameFrom, renameTo);
                }
                else
                {
                    return "450 Requested file action not taken";
                }

                return "250 Requested file action okay, completed";
            }

            return "450 Requested file action not taken";
        }
    }
}
