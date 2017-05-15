using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Server
{
    class PASScommand : Command
    {
        public override string Execute(string arguments)
        {
            if (true)
            {
                return "230 ACCESS GRANTED";
            }
            else
            {
                return "530 ACCESS DENIED";
            }
        }     
    }
}
