using System;

namespace Queue.Algorithm.Testing
{
    class Program
    {
        private static int _inLine;
        private const int MaxValue = 100;
        private const int Max = 10000;

        static void Main(string[] args)
        {
            var cockroach = new ExampleCockroach(MaxValue);

            for (int i = 0; i < Max; i++)
            {
                var best = cockroach.GetNext();
                PrintState(best.State);
            }

            Console.ReadLine();
        }

        private static void PrintState(int[] best)
        {
            Console.Write("  ");
            foreach (int t in best)
                Console.Write("{0} ", t.ToString().PadRight(3));

            Console.Write(" -> {0} ", ExampleCockroach.GetFunctionValue(best).ToString("F").PadRight(3));

            WriteNewlineIfNecessary();
        }

        private static void WriteNewlineIfNecessary()
        {
            if (_inLine == 1)
            {
                Console.WriteLine();
                _inLine = 0;
            }
            else
                _inLine ++;
        }
    }
}
