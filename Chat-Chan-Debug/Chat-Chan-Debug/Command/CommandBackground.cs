using System;
using System.Collections.Generic;
using System.Text;

namespace Chat_Chan_Debug.Command
{
    public class CommandBackground : IBaseCommand
    {
        public string GetName() => "background";
        public string[]? GetAlias() => new string[] { "back", "b" };
        public string GetHelp() => "Background thread if connected.";
        public bool IsArgument() => false;

        public CommandResult Execute(string label, string[] args)
        {
            return CommandResult.Success;
        }
    }
}
