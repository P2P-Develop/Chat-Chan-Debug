namespace Chat_Chan_Debug.Commands
{
    public class CommandPowerlinePrompt : IBaseCommand
    {
        public string GetName() => "powerline";
        public string[]? GetAlias() => new string[] { "pprompt" };
        public string GetHelp() => "Switch powerline used prompt.";
        public bool IsArgument() => false;

        public CommandResult Execute(string label, string[] args)
        {
            Program.defaultPromptFlag = false;
            Logger.Log("Prompt switched successfly.", Log.Info);
            return CommandResult.Success;
        }
    }
}
