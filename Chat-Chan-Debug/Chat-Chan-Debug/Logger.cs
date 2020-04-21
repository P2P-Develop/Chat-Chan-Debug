using System;

namespace Chat_Chan_Debug
{
    public static class Logger
    {
        public static void Log(string value, Log log)
        {
            switch (log)
            {
                case Chat_Chan_Debug.Log.Info:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[*]");
                    break;
                case Chat_Chan_Debug.Log.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("[*]");
                    break;
                case Chat_Chan_Debug.Log.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[*]");
                    break;
                case Chat_Chan_Debug.Log.Add:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("[+]");
                    break;
                case Chat_Chan_Debug.Log.Remove:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("[-]");
                    break;
                case Chat_Chan_Debug.Log.None:
                    break;
                default:
                    return;
            }

            Console.ResetColor();
            Console.WriteLine(value);
        }
    }

    public enum Log
    {
        Info,
        Warning,
        Error,
        Add,
        Remove,
        None
    }
}
