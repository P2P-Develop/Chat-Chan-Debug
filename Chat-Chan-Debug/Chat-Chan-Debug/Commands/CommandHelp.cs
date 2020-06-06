using Chat_Chan_Debug.Exceptions;
using System;
using System.Collections.Generic;

namespace Chat_Chan_Debug.Commands
{
    public class CommandHelp : IBaseCommand
    {
        public string GetName() => "help";

        public string[]? GetAlias() => new[] { "?" };

        public string GetHelp() => "<cmd> - Display this help.\n## <cmd> - The command to reference.";

        public bool IsArgument() => true;

        private const string H1 = "# ";
        private const string H2 = "## ";

        public CommandResult Execute(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[Help]");
            Console.ResetColor();

            var commandList = Program.manager?.GetCommandList();
            if (string.IsNullOrEmpty(args[0]) || string.IsNullOrWhiteSpace(args[0]))
            {
                GetAllHelp(commandList);
                return CommandResult.Success;
            }

            foreach (IBaseCommand command in commandList)
            {
                if (command.GetName() != args[0]) continue;
                if (!command.IsArgument())
                    Console.WriteLine(H1 + command.GetName() + " - " + command.GetHelp());
                else
                    Console.WriteLine(H1 + command.GetName() + " " + command.GetHelp());
                Console.WriteLine(H2 + "Alias: " + string.Join(", ", command.GetAlias() ?? Array.Empty<string>()));
                return CommandResult.Success;
            }
            throw new InvalidArgumentException();
        }

        private static void GetAllHelp(List<IBaseCommand>? commandList)
        {
            if (commandList == null) return;
            foreach (IBaseCommand command in commandList)
            {
                Console.WriteLine(H1 + command.GetName() + " " + command.GetHelp());
                Console.WriteLine(H2 + "Alias: " + string.Join(", ", command.GetAlias() ?? Array.Empty<string>()));
            }
        }
    }
}
