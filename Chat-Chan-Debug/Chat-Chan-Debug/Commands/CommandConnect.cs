﻿using Chat_Chan_Debug.Exceptions;
using System;

namespace Chat_Chan_Debug.Commands
{
    public class CommandConnect : IBaseCommand
    {
        public string GetName() => "connect";

        public string[]? GetAlias() => new[] { "c" };

        public string GetHelp() => "[ADDR|HOST] <-u|--user> <-s|--server> - Connect server call port.\n## [ADDR|HOST] - Access point IP-Address or Host name.\n## <-u|--user> - Connect user id. If you entered username first, you will not need to enter it again.\n## <-s|--server> - Connection server port.";

        public bool IsArgument() => true;

        public CommandResult Execute(string[] args)
        {
            if (!Program.connectedFlag)
            {
                return CommandResult.Success;
            }

            throw new ConnectedException();
        }

        private void Connect(string host, int port)
        {
            Console.WriteLine($"Chat-Chan Connecting Service\nHost: {host}\nPort: {port}");
        }
    }
}
