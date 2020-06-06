namespace Chat_Chan_Debug.Commands
{
    public class CommandExit : IBaseCommand
    {
        public string GetName() => "exit";

        public string[]? GetAlias() => new[] { "quit", "q" };

        public string GetHelp() => "Close the console.";

        public bool IsArgument() => false;

        public CommandResult Execute(string[] args)
        {
            if (Program.connectedFlag)
                Program.quitFlag = true;
            Program.quitFlag = true;
            return CommandResult.Success;
        }
    }
}
