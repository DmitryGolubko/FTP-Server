using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTP_Server
{
    class CommandDictionary : Dictionary<string, Command>
    {
        public CommandDictionary()
        {
            Add("USER", new USERcommand());
            Add("PASS", new PASScommand());
            Add("SYST", new SYSTcommand());
        }
    }
}
