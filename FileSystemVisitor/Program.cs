using System;
using System.IO;
using System.Reflection;

namespace FileSystemVisitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projectName = Assembly.GetCallingAssembly().GetName().Name;
            var startPath = baseDirectory.Substring(0, baseDirectory.IndexOf(projectName) + projectName.Length);

            var fileSystemVisitor = new FileSystemVisitor(startPath, file =>
            {
                return file.FullName.EndsWith(".cs");
            });
            

            fileSystemVisitor.FileFinded += (object sender, ItemFindedEventArgs args) =>
            {
                var fileSystemVisitor = sender as FileSystemVisitor;

                if (fileSystemVisitor != null && args.Path.Contains("Program"))
                {
                    fileSystemVisitor.IsItemExcluded = true;
                }
            };

            foreach (FileSystemInfo item in fileSystemVisitor)
            {
                Console.WriteLine(item.FullName);
            }
        }
    }
}
