namespace Chat_Chan_Debug.Commands
{
    public class CommandCUIPrompt : IBaseCommand
    {
        public string GetName() => "cuiprompt";
        public string[]? GetAlias() => new string[] { "cprompt" };
        public string GetHelp() => "Switch character only prompt.";
        public bool IsArgument() => false;

        public CommandResult Execute(string label, string[] args)
        {
            Program.defaultPromptFlag = true;
            Logger.Log("Prompt switched successfly.", Log.Info);
            return CommandResult.Success;
        }
    }
}
