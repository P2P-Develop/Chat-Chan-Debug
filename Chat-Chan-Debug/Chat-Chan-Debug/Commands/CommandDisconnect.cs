using System;
using System.Collections.Generic;
using System.Text;

namespace Chat_Chan_Debug.Commands
{
    class CommandDisconnect : IBaseCommand
    {
        public string GetName() => "disconnect";
        public string[]? GetAlias() => new string[] { "dc" };
        public string GetHelp() => "Disconnect from server.";
        public bool IsArgument() => false;

        public CommandResult Execute(string label, string[] args)
        {
            if (Program.connectedFlag)
            {

            }
            return CommandResult.Success;
        }
    }
}
