using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Queue.Algorithm.Data;
using Queue.ConsoleUI.Data;

namespace Queue.ConsoleUI
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Add xml files to JacksonInput folder to use them");
                Console.WriteLine("Available xml files:");
                string path = Directory.GetCurrentDirectory();
                path = path.Remove(path.Length - 9);
                path = path + "JacksonInput";
                string[] fileEntries = Directory.GetFiles(path, "*.xml");
                foreach (var file in fileEntries)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }
                Console.WriteLine("Which file do you want to use? Type name with extension");
                var fileToOpen = Console.ReadLine();
                XmlDataLoader xmlDataLoader = new XmlDataLoader();
                Input testInput = xmlDataLoader.LoadInputForJackson(path + "\\" + fileToOpen);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
