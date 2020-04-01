using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;

namespace Chat_Chan_Debug
{
    public static class Commands
    {
        private static TcpClient call = new TcpClient();
        private static TcpClient chat = new TcpClient();
        private static TcpClient command = new TcpClient();
        private static readonly SecureString decryptKey = new SecureString();
        private static readonly SecureString encryptKey = new SecureString();
        private static JoinParser? tokenResponse;
        private static int[] port = new int[1];

        public static CommandResult Execute(string[] command)
        {
            try
            {
                switch (command[0])
                {
                    case "help":
                        if (command.Length == 1)
                        {
                            return Help();
                        }
                        else if (command.Length == 2 && !string.IsNullOrEmpty(command[1]))
                        {
                            return Help(command[1]);
                        }
                        else
                        {
                            Console.WriteLine("Unknown command.\nType 'help' or '?' to get help.");
                            return CommandResult.Error;
                        }
                    case "?":
                        return Help();
                    case "connect":
                        if (command.Length == 4 && (command[2] == "-u" || command[2] == "--user") && !string.IsNullOrEmpty(command[3]))
                        {
                            Program.user = command[3];
                            return Connect(command[1]);
                        }
                        else if (command.Length == 3 && (command[1] == "-s" || command[1] == "--server") && !string.IsNullOrEmpty(command[2]))
                        {
                            if (Program.addr == null)
                                Console.WriteLine("Specify only the IP address and the -u argument to connect.");
                            else
                            {
                                switch (command[2])
                                {
                                    case "call":
                                        return Connect(Program.user, 1);
                                    case "chat":
                                        return Connect(Program.user, 2);
                                    case "command":
                                        return Connect(Program.user, 3);
                                    default:
                                        Console.WriteLine("Unknown server name.");
                                        return CommandResult.Error;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid argument(s).\nType 'help' or '?' to get help.");
                            return CommandResult.Error;
                        }
                        return CommandResult.Error;
                    case "next":
                        if (Program.connectedFlag)
                        {
                            return Connect(Program.user, Program.phase);
                        }
                        else
                        {
                            Console.WriteLine("Not connected.");
                            return CommandResult.Error;
                        }
                    case "exit":
                        Exit();
                        break;
                    case "quit":
                        Exit();
                        break;
                    case "q":
                        Exit();
                        break;
                    case "":
                        break;
                    default:
                        Console.WriteLine("Unknown command.\nType 'help' or '?' to get help.");
                        return CommandResult.Error;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return CommandResult.Fatal;
        }

        private static CommandResult Help()
        {
            Console.ResetColor();
            Console.WriteLine("[Help]\n# help - Display this help.\n## alias: ?\n# connect [ADDR|HOST] <-u|--user> <-s|--server> - Connect server call port.\n## [ADDR|HOST] - Access point IP-Address or Host name.\n## <-u|--user> - Connect user id. If you entered username first, you will not need to enter it again.\n## <-s|--server> - Connection server port.\n## alias: c\n# exit - Close the console.\n## alias: quit, q");
            return CommandResult.Success;
        }
        private static CommandResult Help(string args)
        {
            try
            {
                Console.ResetColor();
                switch (args)
                {
                    case "help":
                        Console.WriteLine("[Help]\n# help - Display this help.\n## alias: ?");
                        break;
                    case "?":
                        Console.WriteLine("[Help]\n# help - Display this help.\n## alias: ?");
                        break;
                    case "connect":
                        Console.WriteLine("[Help]\n# connect [ADDR|HOST] <-u|--user> <-s|--server> - Connect server call port.\n## [ADDR|HOST] - Access point IP-Address or Host name.\n## <-u|--user> - Connect user id. If you entered username first, you will not need to enter it again.\n## <-s|--server> - Connection server port.\n## alias: c");
                        break;
                    case "c":
                        Console.WriteLine("[Help]\n# connect [ADDR|HOST] <-u|--user> <-s|--server> - Connect server call port.\n## [ADDR|HOST] - Access point IP-Address or Host name.\n## <-u|--user> - Connect user id. If you entered username first, you will not need to enter it again.\n## <-s|--server> - Connection server port.\n## alias: c");
                        break;
                    case "exit":
                        Console.WriteLine("[Help]\n# exit - Close the console.\n## alias: quit, q");
                        break;
                    case "quit":
                        Console.WriteLine("[Help]\n# exit - Close the console.\n## alias: quit, q");
                        break;
                    case "q":
                        Console.WriteLine("[Help]\n# exit - Close the console.\n## alias: quit, q");
                        break;
                    default:
                        throw new ArgumentException();
                }
                return CommandResult.Success;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid argument(s).\nType 'help' or '?' to get help.");
                throw;
            }
        }

        private static CommandResult Connect(string addr)
        {
            if (string.IsNullOrEmpty(addr))
            {
                Console.WriteLine("Please input ip-address.");
                return CommandResult.Error;
            }
            else if (addr == "localhost")
            {
                addr = "127.0.0.1";
            }
            else if (!IPAddress.TryParse(addr, out _))
            {
                IPAddress[] _addr = Dns.GetHostAddresses(addr);
                addr = _addr[0].ToString();
            }

            try
            {
                call = new TcpClient(addr, 41410);
            }
            catch (Exception)
            {
                Console.WriteLine("Server port closed");
                return CommandResult.Error;
            }

            Program.phase = 0;
            Program.addr = addr;
            Program.connectedFlag = true;
            Console.WriteLine("Connect Phase 0 Successfly completed.\nType 'next' or 'connect -p 1' to Transition into Phase 1.");
            return CommandResult.Success;
        }
        private static CommandResult Connect(string user, int phase)
        {
            try
            {
                MemoryStream? ms;
                NetworkStream ns;
                byte[]? _sendBytes, resBytes;
                int resSize;
                string? connectedJson = null;
                switch (phase)
                {
                    case 1:
                        return ConnectPhase_1(user, out ms, out ns, out _sendBytes, out resBytes, out resSize,
                                              connectedJson: ref connectedJson);
                    case 2:
                        return ConnectPhase_2(user, out ms, out ns, out _sendBytes, out resBytes, out resSize,
                                              connectedJson: ref connectedJson);
                    case 3:
                        return ConnectPhase_3(user, out ms, out ns, out _sendBytes, out resBytes, out resSize,
                                              connectedJson: ref connectedJson);
                    default:
                        Console.WriteLine("Unknown text.");
                        return CommandResult.Error;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Server port closed");
                return CommandResult.Error;
            }
        }

        private static CommandResult ConnectPhase_3(string user, out MemoryStream? ms, out NetworkStream ns, out byte[]? _sendBytes, out byte[]? resBytes, out int resSize, ref string connectedJson)
        {
            command = new TcpClient(Program.addr, port[1]);
            ns = command.GetStream();
            ms = new MemoryStream();
            _sendBytes = Encoding.UTF8.GetBytes("{ \"exec\": \"join\", \"name\": \"" + user + "\", \"token\": \"" + tokenResponse.Token + "\" }\r\n");
            ns.Write(_sendBytes, 0, _sendBytes.Length);
            _sendBytes = null;
            resBytes = new byte[1024];
            do
            {
                resSize = ns.Read(resBytes, 0, resBytes.Length);
                if (resSize == 0)
                {
                    Console.WriteLine("Server closed.");
                    return CommandResult.Error;
                }
                ms.Write(resBytes, 0, resSize);
            } while (ns.DataAvailable || (resBytes[resSize - 2] != '\r' && resBytes[resSize - 1] != '\n'));
            connectedJson = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
            connectedJson = connectedJson.TrimEnd('\n');
            connectedJson = connectedJson.TrimEnd('\r');
            tokenResponse = JsonConvert.DeserializeObject<JoinParser>(connectedJson);

            Program.phase = 3;
            Console.WriteLine("All server login successfly completed.\nDebug has ended.");
            return CommandResult.Success;
        }

        private static CommandResult ConnectPhase_2(string user, out MemoryStream? ms, out NetworkStream ns, out byte[]? _sendBytes, out byte[]? resBytes, out int resSize, ref string connectedJson)
        {
            chat = new TcpClient(Program.addr, port[0]);
            ns = chat.GetStream();
            ms = new MemoryStream();
            _sendBytes = Encoding.UTF8.GetBytes("{ \"exec\": \"join\", \"name\": \"" + user + "\", \"token\": \"" + tokenResponse.Token + "\" }\r\n");
            ns.Write(_sendBytes, 0, _sendBytes.Length);
            _sendBytes = null;
            resBytes = new byte[1024];
            do
            {
                resSize = ns.Read(resBytes, 0, resBytes.Length);
                if (resSize == 0)
                {
                    Console.WriteLine("Server closed.");
                    return CommandResult.Error;
                }
                ms.Write(resBytes, 0, resSize);
            } while (ns.DataAvailable || (resBytes[resSize - 2] != '\r' && resBytes[resSize - 1] != '\n'));
            connectedJson = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
            connectedJson = connectedJson.TrimEnd('\n');
            connectedJson = connectedJson.TrimEnd('\r');
            tokenResponse = JsonConvert.DeserializeObject<JoinParser>(connectedJson);

            Program.phase = 2;
            Console.WriteLine("Chat server successfly connected.\nType 'next' or 'connect -s command' to Connect command server.");
            return CommandResult.Success;
        }

        private static CommandResult ConnectPhase_1(string user, out MemoryStream? ms, out NetworkStream ns, out byte[]? _sendBytes, out byte[] resBytes, out int resSize, ref string connectedJson)
        {
            ns = call.GetStream();
            ns.ReadTimeout = 20000;
            ns.WriteTimeout = 15000;
            ms = new MemoryStream();
            Encoding enc = Encoding.UTF8;
            if (string.IsNullOrEmpty(user))
                Program.user = "DEBUG-TEST-USR";
            else
                Program.user = user;
            _sendBytes = enc.GetBytes("{ \"exec\": \"join\", \"name\": \"" + user + "\" }\r\n");
            ns.Write(_sendBytes, 0, _sendBytes.Length);
            _sendBytes = null;
            resBytes = new byte[1024];
            do
            {
                resSize = ns.Read(resBytes, 0, resBytes.Length);
                if (resSize == 0)
                {
                    Console.WriteLine("Server closed.");
                    return CommandResult.Error;
                }
                ms.Write(resBytes, 0, resSize);
            } while (ns.DataAvailable || (resBytes[resSize - 2] != '\r' && resBytes[resSize - 1] != '\n'));
            connectedJson = Encoding.UTF8.GetString(ms.GetBuffer(), 0, (int)ms.Length);
            connectedJson = connectedJson.TrimEnd('\n');
            connectedJson = connectedJson.TrimEnd('\r');
            tokenResponse = JsonConvert.DeserializeObject<JoinParser>(connectedJson);
            foreach (char decryptChar in tokenResponse.DecryptKey.ToCharArray())
            {
                decryptKey.AppendChar(decryptChar);
            }
            tokenResponse.DecryptKey = null;
            foreach (char encryptChar in tokenResponse.EncryptKey.ToCharArray())
            {
                encryptKey.AppendChar(encryptChar);
            }
            tokenResponse.EncryptKey = null;
            port = tokenResponse.Port;

            Program.phase = 1;
            Console.WriteLine("Call server successfly connected.\nType 'next' or 'connect -s chat' to Connect chat server.");
            return CommandResult.Success;
        }

        private static void Exit()
        {
            Program.quitFlag = true;
        }
    }

    public enum CommandResult
    {
        Success,
        Warning,
        Error,
        Fatal
    }
}
