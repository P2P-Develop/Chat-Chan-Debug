using Chat_Chan_Debug.Exceptions;
using System;
using System.Collections.Generic;

namespace Chat_Chan_Debug.Commands
{
    public class CommandHelp : IBaseCommand
    {
        public string GetName() => "help";
        public string[]? GetAlias() => new string[] { "?" };
        public string GetHelp() => "<cmd> - Display this help.\n## <cmd> - The command to reference.";
        public bool IsArgument() => true;

        
        private const string h1 = "# ";
        private const string h2 = "## ";
        public CommandResult Execute(string label, string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[Help]");
            Console.ResetColor();

            List<IBaseCommand> commandList = Program.manager.GetCommandList();
            if (string.IsNullOrEmpty(args[0]) || string.IsNullOrWhiteSpace(args[0]))
            {
                GetAllHelp(commandList);
                return CommandResult.Success;
            }

            try
            {
                foreach (IBaseCommand command in commandList)
                {
                    if (command.GetName() == args[0])
                    {
                        if (!command.IsArgument())
                            Console.WriteLine(h1 + command.GetName() + " - " + command.GetHelp());
                        else
                            Console.WriteLine(h1 + command.GetName() + " " + command.GetHelp());
                        Console.WriteLine(h2 + "Alias: " + string.Join(", ", command.GetAlias()));
                        return CommandResult.Success;
                    }
                }
                throw new InvalidArgumentException();
            }
            catch (InvalidArgumentException)
            {
                throw;
            }
        }

        private static void GetAllHelp(List<IBaseCommand> commandList)
        {
            foreach (IBaseCommand command in commandList)
            {
                Console.WriteLine(h1 + command.GetName() + " " + command.GetHelp());
                Console.WriteLine(h2 + "Alias: " + string.Join(", ", command.GetAlias()));
            }
        }
    }
}
