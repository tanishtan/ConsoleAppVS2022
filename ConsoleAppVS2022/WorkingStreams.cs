using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    internal class WorkingStreams
    {
        internal static void Test()
        {
            //TestFileStreamObjectAsync();
            //TestMemoryStream();
            //TestTextBasedOperation();
            TestBinaryReaderWriters();
        }

        static void TestBinaryReaderWriters()
        {
            int num = 9876;
            double d = 1234.56;
            string text = "Hello World";
            Console.WriteLine($"Before Int: {num}, Double: {d}, String: {text}");
            string folder = @"C:\TestFolder";
            string fileName = "BinaryReaderWriter.bin";
            string filePath = Path.Combine(folder, fileName);
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using(BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(num);
                    writer.Write(d);
                    writer.Write(text);
                    writer.Flush();
                }
            }
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    var x = reader.ReadInt32();
                    var y = reader.ReadDouble();
                    var z = reader.ReadString();
                    Console.WriteLine($"After Int: {x}, Double: {y}, String: {z}");
                }
            }
        }

        static void TestTextBasedOperation()
        {
            string folder = @"C:\TestFolder";
            string fileName = "SampleFile.txt";
            string filePath = Path.Combine(folder, fileName);
            Console.WriteLine("Enter the content to be written into the file");
            string content = Console.ReadLine();
            using(StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(content);
                writer.WriteLine(content);
                writer.Flush();
            }
            Console.WriteLine("Contents returned to file. Press a key to begin reading");
            Console.ReadKey();
            using(StreamReader reader = new StreamReader(filePath))
            {
                var line= string.Empty;
                int cnt = 1;
                do
                {
                    line = reader.ReadLine();
                    Console.WriteLine($"Line {cnt++}\n{line}");
                } while (!reader.EndOfStream);

                reader.BaseStream.Seek(0,SeekOrigin.Begin);
            }
        }

        static void TestFileStreamObjectAsync()
        {
            string folder = @"C:\TestFolder";
            string fileName = "SampleFile.txt";
            string filePath = Path.Combine(folder, fileName);
            Console.WriteLine("Enter the content to be written into the file");
            string content = Console.ReadLine();
            using(FileStream fs1 = new FileStream(path: filePath, mode: FileMode.Create,access:FileAccess.ReadWrite))
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(content);
                fs1.Write(buffer: bytes, offset: 0, count: bytes.Length);

                fs1.Flush(); // contents to be written is in buffer, which has to be written to the file
                fs1.Close(); //Mandatory to include the close(), if it is not within the using block
                //within the using block, the compiler will write the close() call.
            }
            Console.WriteLine("File contents written and File closed");
            Console.WriteLine("Press a key to continue");
            Console.ReadKey();

            FileStream fs2 = File.OpenRead(filePath); // file mode is Open, file access is Read
            int length = (int)fs2.Length;
            byte[] buffer = new byte[length];
            fs2.Read(buffer: buffer,offset: 0,count: length);
            content = Encoding.UTF8.GetString(buffer);
            fs2.Close();
            Console.WriteLine($"File Content:\n{content}\nEnd of file content");

        }

        static void TestMemoryStream()
        {
            string content = "The quick brown fox jumps over the lazy dog";
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            MemoryStream ms = new MemoryStream();
            ms.Write(buffer, 0, buffer.Length);
            ms.Seek(10, SeekOrigin.Begin); // position the pointer to the 10th byte from beginning of the file
            buffer = new byte[ms.Length];
            ms.Read(buffer, 0, 10);// offset: 0 -- current position (10) - read 10 bytes from 10th position
            content = Encoding.UTF8.GetString(buffer);
            Console.WriteLine("Content in memory stream {0}",content);
            ms.Close();

        }
    }

}
