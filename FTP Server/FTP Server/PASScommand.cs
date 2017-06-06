using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Server
{
    class PASScommand : Command
    {
        public override string Execute(string password, ClientConnection connection)
        {
            connection.currentUser = UserStore.Validate(connection.connectedClientusername, password);
            connection.currentDirectory = connection.root;

            if (connection.currentUser != null)
            {
                
                return "230 ACCESS GRANTED";
            }
            else
            {
                return "230 ANONYMOUS ACCESS GRANTED";
            }
        }     
    }
}
