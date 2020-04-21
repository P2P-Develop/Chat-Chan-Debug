using Chat_Chan_Debug.Command;
using Chat_Chan_Debug.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;

namespace Chat_Chan_Debug
{
    internal class Program
    {
        #region Public valiables

        public static bool quitFlag = false;
        public static bool connectedFlag = false;
        public static int phase = -1;
        public static string? addr;
        public static string? user;
        public static string? version;
        public static bool defaultPromptFlag = false;

        #endregion

        #region Public Instances

        public static CommandManager? manager;
        public static TcpClient? call;
        public static TcpClient? chat;
        public static TcpClient? command;

        #endregion

        #region Private instances

        private static Assembly? asm;
        private static Version? ver;

        #endregion

        #region Main

        internal static void Main()
        {
            Console.WriteLine("Chat-Chan Debug Application\nCreated by P2PDevelop\nThis Application are managed by MIT License.\n\n");
            Load();
            while (!quitFlag)
            {
                try
                {
                    Prompt.DisplayPrompt(new Dictionary<string, ConsoleColor>() { { ver.ToString(), ConsoleColor.Blue }, { "Connecting", ConsoleColor.Red } });
                    string[] _cmd = Console.ReadLine().Split();
                    switch (manager.Execute(_cmd[0], _cmd))
                    {
                        case Command.CommandResult.Success:
                            continue;
                        case Command.CommandResult.Warning:
                            continue;
                        case Command.CommandResult.Error:
                            continue;
                        case Command.CommandResult.Fatal:
                            quitFlag = true;
                            break;
                        default:
                            continue;
                    }
                }
                catch (InvalidArgumentException)
                {
                    Console.WriteLine("Invalid argument(s). type \"help\" or \"?\" to get help.");
                    continue;
                }
                catch (CommandNotFoundException)
                {
                    Console.WriteLine("Unknown command. type \"help\" or \"?\" to get help.");
                    continue;
                }
                catch (PromptException)
                {
                    defaultPromptFlag = true;
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.ResetColor();
                    Console.WriteLine("Some error occurred while displaying the prompt, so change to a prompt that contains only characters.");
                    continue;
                }
                catch (ConnectedException)
                {
                    Console.WriteLine("Already connected.");
                    continue;
                }
                catch (Exception e)
                {
                    Logger.Log(e.ToString(), Log.Error);
                    continue;
                }
            }
        }

        #endregion

        #region Loads

        private static void Load()
        {
            asm = Assembly.GetExecutingAssembly();
            ver = asm.GetName().Version;
            version = ver.ToString();
            manager = new CommandManager();
            CheckError();
            ListenCommand();
        }

        private static void CheckError()
        {

        }

        private static void ListenCommand()
        {
            manager.ListenCommand(new CommandConnect());
            manager.ListenCommand(new CommandHelp());
            manager.ListenCommand(new CommandGetCode());
            manager.ListenCommand(new CommandExit());
        }

        #endregion

    }
}
