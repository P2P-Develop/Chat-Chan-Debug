using Chat_Chan_Debug.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat_Chan_Debug.Command
{
    public class CommandConnect : IBaseCommand
    {
        public string GetName() => "connect";
        public string[]? GetAlias() => new string[] { "c" };
        public string GetHelp() => "[ADDR|HOST] <-u|--user> <-s|--server> - Connect server call port.\n## [ADDR|HOST] - Access point IP-Address or Host name.\n## <-u|--user> - Connect user id. If you entered username first, you will not need to enter it again.\n## <-s|--server> - Connection server port.";
        public bool IsArgument() => true;

        public CommandResult Execute(string label, string[] args)
        {
            try
            {
                if (!Program.connectedFlag)
                {
                    return CommandResult.Success;
                }
                else
                {
                    throw new ConnectedException();
                }
            }
            catch (ConnectedException)
            {
                throw;
            }
        }

        private void Connect(string host, int port)
        {
            Console.WriteLine("Chat-Chan Connecting Service\nHost: " + host + "\nPort: " + port);
            Logger.Log("Connecting call server...", Log.Info);
            Program.call = new TcpClient(host, port);
        }
        private void Connect(IPAddress addr, int[] ports, int phase)
        {

        }
    }
}
