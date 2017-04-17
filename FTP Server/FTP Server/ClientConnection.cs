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
        private string connectedClientusername;

        public ClientConnection(TcpClient client)
        {
            connectedClient = client;
            stream = connectedClient.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        public void HandleClient(object obj)
        {
            writer.WriteLine("220 Service Ready.");
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
                    
                    if (response == null)
                    {
                        switch (cmd)
                        {
                            case "USER":
                                response = User(arguments);
                                break;
                            case "PASS":
                                response = Password(arguments);
                                break;
                            case "CWD":
                                response = ChangeWorkingDirectory(arguments);
                                break;
                            case "PWD":
                                response = "257 \"/\" is current directory.";
                                break;
                            case "QUIT":
                                response = "221 Service closing control connection";
                                break;
                            default:
                                response = "502 Command not supported";
                                break;
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

        private string User(string username)
        {
            connectedClientusername = username;
            return "331 User name ok, need password.";
        }

        private string ChangeWorkingDirectory(string pathname)
        {
            return "250 Changed to new directory";
        }

        private string Password(string password)
        {
            if (true)
            {
                return "230 User logged in";
            }
            else
            {
                return "530 Not logged in";
            }
        }
    }
}
