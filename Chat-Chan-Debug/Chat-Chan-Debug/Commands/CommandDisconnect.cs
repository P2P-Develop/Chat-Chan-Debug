namespace Chat_Chan_Debug.Commands
{
    internal class CommandDisconnect : IBaseCommand
    {
        public string GetName() => "disconnect";

        public string[]? GetAlias() => new[] { "dc" };

        public string GetHelp() => "Disconnect from server.";

        public bool IsArgument() => false;

        public CommandResult Execute(string[] args)
        {
            if (Program.connectedFlag)
            {
            }
            return CommandResult.Success;
        }
    }
}
