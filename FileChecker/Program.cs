using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FileChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        static void Run()
        {
            Console.WriteLine("Please enter the index of category");
            Console.WriteLine("1- Load");
            Console.WriteLine("2- Save");
            Console.WriteLine("3- Exit");
            Console.WriteLine();
            string get = Console.ReadLine();
            Console.WriteLine();
            var i = 0;
            if (!Int32.TryParse(get, out i))
            {
                Console.WriteLine("Invalid number!");
                Console.WriteLine();
                Run();
                return;
            }
            if (i > 3 || i < 1)
            {
                Console.WriteLine("Invalid number!");
                Console.WriteLine();
                Run();
                return;
            }
            if (i == 3)
            {
                Environment.Exit(0);
            }
            else if (i == 2)
            {
                var total = "";
                foreach (var fs in new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).GetFiles())
                {
                    if (fs.Name == ".info") continue;
                    total += fs.Name + "\n";
                }
                if (File.Exists(".info"))
                    File.Delete(".info");
                using (var sw = new StreamWriter(".info"))
                    sw.Write(total);
                Console.WriteLine("Created the info file!");
                Console.WriteLine();
                Run();
                return;
            }
            else if (i == 1)
            {
                if (!File.Exists(".info"))
                {
                    Console.WriteLine("Info file doesn't exists!");
                    Console.WriteLine();
                    Run();
                    return;
                }
                var Files = new List<String>();
                using (var sr = new StreamReader(".info"))
                {
                    var line = "";
                    while ((line = sr.ReadLine()) != null)
                        if (!String.IsNullOrEmpty(line))
                            Files.Add(line);
                }
                Files.Add(".info");
                var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).GetFiles();
                if (dir.Length == Files.Count)
                {
                    Console.WriteLine("All the files fit!");
                    Console.WriteLine();
                    Run();
                    return;
                }
                if (dir.Length > Files.Count)
                {
                    Console.WriteLine("There are some files missing!");
                    Console.WriteLine("Wrote them into .info");
                    File.Delete(".info");
                    var nFiles = new List<String>();
                    foreach (var db in dir)
                        nFiles.Add(db.Name);
                    foreach (var db in Files)
                        if (nFiles.Contains(db))
                            nFiles.Remove(db);
                    using (var sw = new StreamWriter(".info"))
                        foreach (var db in nFiles)
                            sw.WriteLine(db);
                    Console.WriteLine();
                    Run();
                    return;
                }
            }
        }
    }
}
