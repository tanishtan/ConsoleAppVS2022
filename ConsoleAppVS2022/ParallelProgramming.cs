using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    internal class ParallelProgramming
    {
        static string ConvertBytesToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.AppendFormat("{0:X}", bytes[i]);
            }
            return sb.ToString();
        }
        internal static void Test()
        {
            Console.WriteLine("Calling Seqeuntial Loop Function. Press a key to start.");
            Console.ReadKey();

            SequentialLoop();
            Console.WriteLine("Sequential Loop execution completed. Press a key to start the next task.");
            Console.ReadKey();

            ParallelLoop();
            Console.WriteLine("Parallel Loop execution completed. Press a key to start the next task.");
            Console.ReadKey();
        }
        static int MaxSize = 90_00_000;
        static void SequentialLoop()
        {
            Console.WriteLine("Sequential Loop begins execution");
            System.Diagnostics.Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < MaxSize; i++)
            {
                var aes = Aes.Create();
                aes.GenerateIV();
                var buffer = aes.Key;
                var str = ConvertBytesToHexString(buffer);
            }
            watch.Stop();
            Console.WriteLine($"{nameof(SequentialLoop)} has taken {watch.ElapsedMilliseconds} milliseconds.");
        }

        static void ParallelLoop()
        {
            Console.WriteLine("Parallel Loop begins execution");
            System.Diagnostics.Stopwatch watch = Stopwatch.StartNew();
            //for (int i = 0; i < MaxSize; i++)
            Parallel.For(1, MaxSize + 1, i =>
            {
                var aes = Aes.Create();
                aes.GenerateIV();
                aes.GenerateKey();
                var buffer = aes.Key;
                var str = ConvertBytesToHexString(buffer);
            });
            watch.Stop();
            Console.WriteLine($"{nameof(ParallelLoop)} has taken {watch.ElapsedMilliseconds} ms.");
        }


    }
}