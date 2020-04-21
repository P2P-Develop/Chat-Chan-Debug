﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chat_Chan_Debug.Command
{
    public class CommandExit : IBaseCommand
    {
        public string GetName() => "exit";
        public string[]? GetAlias() => new string[] { "quit", "q" };
        public string GetHelp() => "Close the console.";
        public bool IsArgument() => false;

        public CommandResult Execute(string label, string[] args)
        {
            if (Program.connectedFlag)
                Program.quitFlag = true;
            Program.quitFlag = true;
            return CommandResult.Success;
        }
    }
}
