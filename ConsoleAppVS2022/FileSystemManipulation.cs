using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleAppVS2022
{
    internal class FileSystemManipulation
    {
        internal static void Test()
        {
            //TestDirectory();
            //TestFiles();
            //TestFileSystemInfo();
            //TestDriveInfo();
            TestSystemWatcher();
        }
        static void TestDirectory()
        {
            string folderName = @"C:\TestFolder"; //@ - quoted strings are not parsed by compiler
            //absolute pathing
            if (Directory.Exists(path: folderName))
            {
                Directory.Delete(path: folderName, recursive: true);
            }
            Directory.CreateDirectory(path: folderName);
            var currentApplicationFolder = "../../../";//gives acces to parent folder (relative path)
            var currFolder = Directory.GetCurrentDirectory(); // returns current directory from where exe is running
            var anyDir = Environment.CurrentDirectory;

            var files = Directory.GetFiles(path: currentApplicationFolder);
            Console.WriteLine($"Files in folder [{currentApplicationFolder}]");
            var counter = 1;
            foreach (var file in files)
            {
                Console.WriteLine($"{counter++}. {Path.GetFileName(path: file)}");
                Console.WriteLine("\tFull path: {0}", file);
            }
        }
        static void TestFiles()
        {
            string folderName = @"C:\TestFolder";                                                      
            if (Directory.Exists(path: folderName))
            {
                Directory.Delete(path: folderName, recursive: true);
            }
            Directory.CreateDirectory(path: folderName);
            for(int i = 1;i<11;i++)
            {
                var fileName = Path.Combine(folderName, $"FileName_{i}.txt");
                if(File.Exists(fileName))
                    File.Delete(fileName);
                File.Create(fileName).Close(); // touch the file. Create an empty file
            }
            // FIle.copy can also be used
            var srcFolder = "../../../";
            var files = Directory.GetFiles(path:srcFolder, searchPattern: "*.cs");
            foreach( var file in files)
            {
                //copy only .cs files, ignore others
                if (Path.GetExtension(file) == ".cs")
                {
                    var desFileName = Path.Combine(folderName, Path.GetFileName(file));
                    File.Copy(sourceFileName: file, destFileName: desFileName);
                }
            }
        }

        static void TestFileSystemInfo()
        {
            string folderName = @"C:\TestFolder";
            DirectoryInfo dInfo = new DirectoryInfo(path: folderName);
            FileInfo[] files = dInfo.GetFiles(searchPattern: "*.txt");
            foreach( FileInfo file in files)
            {
                Console.WriteLine("{0}, size:{1}, Extn: {2}, Dir: {3}",file.Name, file.Length,file.Extension,file.DirectoryName);
            }
        }

        static void TestDriveInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Listing all the drives\n");
            foreach (DriveInfo drive in drives)
            {
                sb.AppendLine($"Drive name: {drive.Name}")
                    .AppendLine($"total size: {drive.TotalSize/ (1024*1024*1024)} GB")
                    .AppendLine($"available space: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)}GB")
                    .AppendLine($"volume label: {drive.VolumeLabel}")
                    .AppendLine($"Drive details: {drive.DriveType} - {drive.DriveFormat}")
                    .AppendLine();
            }
            Console.WriteLine( sb.ToString() );
        }

        static void TestSystemWatcher()
        {
            string folderName = @"C:\TestFolder";
            using(var fsw = new FileSystemWatcher(folderName))
            {
                fsw.NotifyFilter = NotifyFilters.FileName |
                    NotifyFilters.DirectoryName |
                    NotifyFilters.CreationTime |
                    NotifyFilters.LastWrite |
                    NotifyFilters.LastAccess |
                    NotifyFilters.Attributes |
                    NotifyFilters.Security;
                fsw.Error += (s, e) =>
                {
                    var ex = e.GetException();
                    if (ex != null)
                    {
                        Console.WriteLine($"Message: {ex.Message}\nStack Trace: {ex.StackTrace}");
                    }
                };
                fsw.Renamed += (s, e) =>
                {
                    Console.WriteLine($"Renamed:\n\tOld Name: {e.OldFullPath}\n\tNew Name: {e.FullPath}");
                };
                fsw.Deleted += (s, e) => Console.WriteLine($"Deleted: {e.FullPath}");
                fsw.Created += (s, e) => Console.WriteLine($"Created: {e.FullPath}");
                fsw.Changed += (s, e) =>
                {
                    if (e.ChangeType != WatcherChangeTypes.Changed) return;
                    Console.WriteLine($"Changed: {e.FullPath}");
                };
                fsw.Filter = "*.txt";
                fsw.IncludeSubdirectories = false;
                fsw.EnableRaisingEvents = true;

                Console.WriteLine("Press a key to terminate");
                Console.ReadKey();
            }
        }
    }
}

