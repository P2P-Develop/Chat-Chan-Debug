﻿namespace Chat_Chan_Debug.Commands
{
    public class CommandBackground : IBaseCommand
    {
        public string GetName() => "background";

        public string[]? GetAlias() => new[] { "back", "b" };

        public string GetHelp() => "Background thread if connected.";

        public bool IsArgument() => false;

        public CommandResult Execute(string[] args)
        {
            return CommandResult.Success;
        }
    }
}
