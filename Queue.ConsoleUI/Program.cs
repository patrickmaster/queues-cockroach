using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Queue.Algorithm.Data;
using Queue.ConsoleUI.DI;
using Queue.ConsoleUI.Solver;

namespace Queue.ConsoleUI
{
    internal class Program
    {
        private static ILifetimeScope _scope;
        private static string _filename;
        private static AlgorithmType _algorithmType;
        private static SolverResult _result;
        private static IEnumerable<string> _xmlFiles;

        private static void Main(string[] args)
        {
            var container = Dependency.Register();

            using (_scope = container.BeginLifetimeScope())
            {
                try
                {
                    LoadXmlFilesAndPrintInfo();
                    GetUserChoice();
                    TrySolveProblem();
                    OutputResults();
                }
                catch (UserInputException exception)
                {
                    Console.WriteLine("Got bad user input: " + exception.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error solving the problem");
                    Console.WriteLine(exception);
                }

                Console.ReadLine();
            }
        }

        private static void LoadXmlFilesAndPrintInfo()
        {
            LoadXmlFiles();
            PrintXmlFiles();
            PrintAlgorithmTypes();
        }

        private static void LoadXmlFiles()
        {
            _xmlFiles = FilesystemHelper.GetXmlFilesInDirectory(FilesystemHelper.DataPath);
        }

        private static void PrintXmlFiles()
        {
            var fileNames = _xmlFiles
                .Select(path => path.Replace(FilesystemHelper.DataPath + @"\", string.Empty))
                .ToArray();

            Console.WriteLine("Found XML files:");
            for (int i = 0; i < fileNames.Length; i++)
                Console.WriteLine("[{0}]: {1}", i + 1, fileNames[i]);
        }

        private static void PrintAlgorithmTypes()
        {
            var algorithms = Enum.GetValues(typeof(AlgorithmType));

            Console.WriteLine("Algorithm types:");
            for (int i = 0; i < algorithms.Length; i++)
                Console.WriteLine("[{0}]: {1}", i + 1, algorithms.GetValue(i));
        }

        private static void GetUserChoice()
        {
            GetFileChoice();
            GetAlgorithmChoice();
        }

        private static void GetFileChoice()
        {
            Console.WriteLine("Which file to use?");
            int fileNumber;
            if (!ConsoleHelper.TryReadInt(out fileNumber) || !TrySetFilename(fileNumber - 1))
                throw new UserInputException("Bad file choice");
        }

        private static void GetAlgorithmChoice()
        {
            Console.WriteLine("Which algorithm to use?");
            int algorithmNumber;
            if (!ConsoleHelper.TryReadInt(out algorithmNumber) || !TrySetAlgorithm(algorithmNumber - 1))
                throw new UserInputException("Bad algorithm choice");
        }

        private static bool TrySetAlgorithm(int algorithmNumber)
        {
            _algorithmType = (AlgorithmType)Enum.Parse(typeof(AlgorithmType), algorithmNumber.ToString());
            return Enum.IsDefined(typeof(AlgorithmType), _algorithmType);
        }

        private static bool TrySetFilename(int fileNumber)
        {
            try
            {
                _filename = _xmlFiles.ElementAt(fileNumber);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void TrySolveProblem()
        {
            var solver = _scope.Resolve<ISolverFacade>();

            _result = solver.Solve(_filename, _algorithmType);
        }

        private static void OutputResults()
        {
            var output = _result.OutputData;

            Console.WriteLine("Time: {0}", output.Time);
            Console.WriteLine("Channels count: {0}", output.ChannelsCount);
            Console.WriteLine("Function value: {0}", output.Value);

            foreach (var system in output.SystemStats)
                PrintResultsFor(system);
        }

        private static void PrintResultsFor(SystemStatistics system)
        {
            Console.WriteLine("\tSystem:");
            Console.WriteLine("\tService time: {0}", system.ServiceTime);
            Console.WriteLine("\tQueue time: {0}", system.QueueTime);
            Console.WriteLine("\tAverage entries count: {0}", system.AverageEntriesCount);
            Console.WriteLine("\tAverage queue length: {0}", system.AverageQueueLength);
        }
    }
}
