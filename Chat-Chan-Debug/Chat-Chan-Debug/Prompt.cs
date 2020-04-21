using Chat_Chan_Debug.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat_Chan_Debug
{
    public class Prompt
    {
        private static readonly char arrow = '';
        private static readonly char space = ' ';

        public static void DisplayPrompt(Dictionary<string, ConsoleColor> pairs)
        {
            try
            {
                if (Program.defaultPromptFlag)
                {
                    DisplayNormalPrompt(pairs.Keys.ToArray());
                    return;
                }
                int treeIndex = 0;
                foreach(KeyValuePair<string, ConsoleColor> pair in pairs)
                {
                    ConsoleColor color = pair.Value;
                    string tree = pair.Key;
                    Console.BackgroundColor = color;
                    Console.Write(space + tree + space);
                    if (pairs.Count == treeIndex + 1)
                    {
                        Console.ResetColor();
                        Console.ForegroundColor = color;
                        Console.Write(arrow);
                        Console.Write(space);
                        Console.ResetColor();
                    }
                    else if (pairs.Count > treeIndex + 1)
                    {
                        ConsoleColor nextColor = pairs.Values.ToArray()[treeIndex + 1];
                        Console.ForegroundColor = color;
                        Console.BackgroundColor = nextColor;
                        Console.Write(arrow);
                        Console.Write(space);
                        Console.ResetColor();
                    }

                    treeIndex++;
                }
            }
            catch (PromptException)
            {
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void DisplayNormalPrompt(string[] trees)
        {
            int treeIndex = 0;

            foreach (string tree in trees)
            {
                if (treeIndex + 1 == trees.Length)
                {
                    Console.Write(tree);
                }
                else
                    Console.Write(tree + "/");
                treeIndex++;
            }

            Console.Write(">");
        }
    }
}
