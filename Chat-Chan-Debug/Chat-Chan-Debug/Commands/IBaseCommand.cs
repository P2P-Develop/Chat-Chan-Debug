using System;
using System.Collections.Generic;
using System.Text;

namespace Chat_Chan_Debug.Commands
{
    public interface IBaseCommand
    {
        public CommandResult Execute(string label, string[] args);

        public string GetName();

        public string[]? GetAlias();

        public string GetHelp();

        public bool IsArgument();

    }
}
