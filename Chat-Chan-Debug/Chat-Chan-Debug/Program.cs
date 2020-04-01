using System;
using System.Reflection;

namespace Chat_Chan_Debug
{
    internal class Program
    {
        public static bool quitFlag;
        public static bool connectedFlag;
        public static int phase = -1;
        public static string? addr;
        public static string? user;
        private static Assembly? asm;
        private static Version? ver;

        #region Main

        internal static void Main()
        {
            Console.WriteLine("Chat-Chan Debug Application\nCreated by P2PDevelop\nThis Application are managed by MIT License.\n\n");
            Load();
            while (!quitFlag)
            {
                Prompt(connectedFlag, phase);
                try
                {
                    switch (Commands.Execute(Console.ReadLine().Split(" ")))
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
                catch (Exception)
                {
                    continue;
                }
            }
        }

        #endregion

        static void Load()
        {
            asm = Assembly.GetExecutingAssembly();
            ver = asm.GetName().Version;
            CheckError();
        }

        static void CheckError()
        {

        }

        static void Prompt(bool connected, int? phase)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Debug-" + ver + ' ');
            if (connected)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write('');
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  " + addr + ' ');
                switch (phase)
                {
                    case -1:
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('');
                        Console.ResetColor();
                        Console.Write(' ');
                        break;
                    case 0:
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('');
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("  Connecting ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write('');
                        Console.ResetColor();
                        Console.Write(' ');
                        break;
                    case 1:
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('');
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("  Call ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('');
                        Console.ResetColor();
                        Console.Write(' ');
                        break;
                    case 2:
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('');
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("  Call & Chat ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('');
                        Console.ResetColor();
                        Console.Write(' ');
                        break;
                    case 3:
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('');
                        Console.ResetColor();
                        Console.Write(' ');
                        break;
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write('');
                Console.ResetColor();
                Console.Write(' ');
            }
        }
    }
}
