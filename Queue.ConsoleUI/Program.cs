using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Queue.ConsoleUI.DI;
using Queue.ConsoleUI.Solver;

namespace Queue.ConsoleUI
{
    internal class Program
    {
        private static ILifetimeScope _scope;
        private static string _filename;

        private static void Main(string[] args)
        {
            var container = Dependency.Register();

            using (_scope = container.BeginLifetimeScope())
            {
                LoadAndPrintXmlFilesInExecutableDirectory();
                GetUserChoice();
                TrySolveProblem();
                OutputResults();
            }
        }

        private static void OutputResults()
        {
            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void TrySolveProblem()
        {
            var solver = _scope.Resolve<ISolver>();

            try
            {
                var result = solver.Solve(_filename);
                PrintResult(result);
            }
            catch (Exception e)
            {
                PrintError(e);
            }   
        }

        private static void PrintResult(SolverResult result)
        {
            var output = result.OutputData;

            Console.WriteLine("Channels count: {0}", output.ChannelsCount);
        }

        private static void GetUserChoice()
        {
        }

        private static void PrintError(Exception exception)
        {
            Console.WriteLine("An error has occured:");
            Console.WriteLine(exception.Message);
        }

        private static void LoadAndPrintXmlFilesInExecutableDirectory()
        {
            Console.WriteLine("Found XML files:");
        }
    }
}
