using System;
using System.Collections.Generic;
using System.Text;

namespace Chat_Chan_Debug.Command
{
    class CommandGetCode : IBaseCommand
    {
        public string GetName() => "getcode";
        public string[]? GetAlias() => new string[] { "code" };
        public string GetHelp() => "Get the code if connected.";
        public bool IsArgument() => false;

        public CommandResult Execute(string label, string[] args)
        {
            return CommandResult.Success;
        }
    }
}
