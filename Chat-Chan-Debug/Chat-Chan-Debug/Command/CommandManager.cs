using Chat_Chan_Debug.Exceptions;
using System.Collections.Generic;

namespace Chat_Chan_Debug.Command
{
    public class CommandManager
    {
        private List<IBaseCommand>? commandList = new List<IBaseCommand>();

        private IBaseCommand? NotFoundCommand { get; set; }

        public void ListenCommand<T>(T commands) where T : IBaseCommand
        {
            commandList.Add(commands);
        }

        public void UnListenCommand<T>(T commands) where T : IBaseCommand
        {
            commandList.Remove(commands);
        }

        public CommandResult Execute(string commandName, string[] arg)
        {
            List<string> args = new List<string>(arg);
            args.RemoveAt(0);
            if (commandName is "")
                return CommandResult.Success;
            try
            {
                foreach (IBaseCommand command in commandList)
                {
                    string[]? aliasList = command.GetAlias();
                    string label = command.GetName();
                    if (label == commandName)
                        return command.Execute(commandName, args.ToArray());
                    else if (aliasList != null)
                    {
                        foreach (string aliasStr in aliasList)
                        {
                            if (aliasStr == commandName)
                                return command.Execute(commandName, args.ToArray());
                        }
                    }
                }
                if (NotFoundCommand != null)
                    return NotFoundCommand.Execute(commandName, args.ToArray());
                else
                    throw new CommandNotFoundException();
            }
            catch (CommandNotFoundException)
            {
                throw;
            }
        }

        public List<IBaseCommand> GetCommandList() => commandList;
    }
}
