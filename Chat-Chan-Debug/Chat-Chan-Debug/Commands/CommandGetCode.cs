namespace Chat_Chan_Debug.Commands
{
    internal class CommandGetCode : IBaseCommand
    {
        public string GetName() => "getcode";

        public string[]? GetAlias() => new[] { "code" };

        public string GetHelp() => "Get the code if connected.";

        public bool IsArgument() => false;

        public CommandResult Execute(string[] args)
        {
            return CommandResult.Success;
        }
    }
}
