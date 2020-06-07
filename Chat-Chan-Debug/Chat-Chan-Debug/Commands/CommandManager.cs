using System;
using Chat_Chan_Debug.Exceptions;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Chat_Chan_Debug.Commands
{
    public class CommandManager
    {
        private readonly List<IBaseCommand>? _commandList = new List<IBaseCommand>();

        private IBaseCommand? NotFoundCommand { get; set; }

        public void ListenCommand<T>(T commands) where T : IBaseCommand
        {
            if (commands != null) _commandList?.Add(commands);
        }

        public void UnListenCommand<T>(T commands) where T : IBaseCommand
        {
            if (commands != null) _commandList?.Remove(commands);
        }

        public CommandResult Execute(string commandName, IEnumerable<string> arg)
        {
            if (commandName == null) throw new ArgumentNullException(nameof(commandName));
            List<string> args = new List<string>(arg);
            if (args == null) throw new ArgumentNullException(nameof(args));
            args.RemoveAt(0);
            if (commandName != null && commandName is "")
                return CommandResult.Success;
            if (_commandList == null)
                return NotFoundCommand?.Execute(args.ToArray()) ?? throw new CommandNotFoundException();
            foreach (IBaseCommand command in _commandList)
            {
                var aliasList = command.GetAlias();
                if (command.GetName() == commandName)
                    return command.Execute(args.ToArray());
                if (aliasList == null) continue;
                if (aliasList.Any(aliasStr => aliasStr == commandName))
                    return command.Execute(args.ToArray());
            }

            return NotFoundCommand?.Execute(args.ToArray()) ?? throw new CommandNotFoundException();
        }

        public List<IBaseCommand> GetCommandList() => _commandList ?? throw new NoNullAllowedException();
    }
}
