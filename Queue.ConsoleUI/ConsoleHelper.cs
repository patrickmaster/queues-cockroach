using System;

namespace Queue.ConsoleUI
{
    internal static class ConsoleHelper
    {
        public static bool TryReadInt(out int fileNumber)
        {
            try
            {
                var line = Console.ReadLine();
                return int.TryParse(line.Trim(), out fileNumber);
            }
            catch
            {
                fileNumber = default(int);
                return false;
            }
        }
    }
}