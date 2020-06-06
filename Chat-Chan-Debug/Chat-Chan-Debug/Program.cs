using Chat_Chan_Debug.Commands;
using Chat_Chan_Debug.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;
using static System.Console;
using static Chat_Chan_Debug.Logger;
using ReadLine = System.ReadLine;

namespace Chat_Chan_Debug
{
    internal static class Program
    {
        #region Public valiables

        public static bool quitFlag = false;
        public static bool connectedFlag = false;
        public static int phase = -1;
        public static string? addr;
        public static string? user;
        public static bool defaultPromptFlag = false;

        #endregion Public valiables

        #region Internal instances

        public static CommandManager? manager;
        internal static TcpClient? call;
        internal static TcpClient? chat;
        internal static TcpClient? command;

        #endregion Internal instances

        #region Private instances

        private static Assembly? _asm;
        private static Version? _ver;

        #endregion Private instances

        #region Main

        internal static void Main()
        {
            Log("Chat-Chan Debug Application\nCreated by P2PDevelop\nThis Application are managed by MIT License.\n\n", Log.None);
            Load();
            while (!quitFlag)
            {
                try
                {
                    Prompt.DisplayPrompt(new Dictionary<string, ConsoleColor>() { { _ver?.ToString(), ConsoleColor.Blue } });
                    string[] cmd = ReadLine.Read().Split();
                    if (manager != null)
                        switch (manager.Execute(cmd[0], cmd))
                        {
                            case CommandResult.Success:
                                continue;
                            case CommandResult.Warning:
                                continue;
                            case CommandResult.Error:
                                continue;
                            case CommandResult.Fatal:
                                quitFlag = true;
                                break;

                            default:
                                continue;
                        }
                }
                catch (InvalidArgumentException)
                {
                    Log("Invalid argument(s). type \"help\" or \"?\" to get help.", Log.Error);
                }
                catch (CommandNotFoundException)
                {
                    Log("Unknown command. type \"help\" or \"?\" to get help.", Log.Error);
                }
                catch (PromptException)
                {
                    defaultPromptFlag = true;
                    SetCursorPosition(0, CursorTop);
                    ResetColor();
                    Log("Some error occurred while displaying the prompt, so change to a prompt that contains only characters.", Log.Error);
                }
                catch (ConnectedException)
                {
                    Log("Already connected.", Log.Warning);
                }
                catch (ServerClosedException)
                {
                    Log("Server closed", Log.Error);
                }
                catch (Exception e)
                {
                    Log(e.ToString(), Log.Error);
                }
            }
        }

        #endregion Main

        #region Loads

        private static void Load()
        {
            _asm = Assembly.GetExecutingAssembly();
            _ver = _asm.GetName().Version;
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
            manager.ListenCommand(new CommandBackground());
            manager.ListenCommand(new CommandDisconnect());
            manager.ListenCommand(new CommandCUIPrompt());
            manager.ListenCommand(new CommandPowerlinePrompt());
        }

        #endregion Loads
    }
}
