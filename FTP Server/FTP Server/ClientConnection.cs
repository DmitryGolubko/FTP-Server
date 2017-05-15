using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace FTP_Server
{
    class ClientConnection
    {
        private TcpClient connectedClient;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        private CommandDictionary commandDictionary = new CommandDictionary();

        public ClientConnection(TcpClient client)
        {
            connectedClient = client;
            stream = connectedClient.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        public void HandleClient(object obj)
        {
            writer.WriteLine("220 SERVICE READY.");
            writer.Flush();

            string line;

            try
            {
                while(!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    string response = null;

                    string[] command = line.Split(' ');
                    string cmd = command[0].ToUpper();
                    string arguments = command.Length > 1 ? line.Substring(command[0].Length + 1) : null;
                    Command currentCommand;
                    if (response == null)
                    {
                        if (commandDictionary.ContainsKey(cmd))
                        {
                            commandDictionary.TryGetValue(cmd, out currentCommand);
                            response = currentCommand.Execute(arguments);
                        }
                        else
                        {
                            response = "502 COMMAND NOT SUPPORTED";
                        }
                    }
                    
                    if (connectedClient == null || !connectedClient.Connected)
                    {
                        break;
                    }
                    else
                    {
                        writer.WriteLine(response);
                        writer.Flush();
                        if (response.StartsWith("221"))
                        {
                            break;
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public void Dispose()
        {
            connectedClient.Close();
            stream.Close();
            reader.Close();
            writer.Close();
        }

    }
}
