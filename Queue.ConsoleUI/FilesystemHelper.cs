using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Queue.ConsoleUI
{
    internal static class FilesystemHelper
    {
        public static string ExecutablePath { get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); } }

        public static string DataPath { get { return Path.Combine(ExecutablePath, "DataFiles"); } }

        public static IEnumerable<string> GetXmlFilesInDirectory(string location)
        {
            var xmlFiles = Directory.GetFiles(location, "*.xml");
            return xmlFiles;
        }
    }
}