using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    internal class MemoryManagement : IDisposable
    {
        private int _counter;
        private readonly int MaxSize = 10_000;
        //10_000, underscore is digit seperator
        //readonly -> like a constant with a leeway
        //          -> readonly var can be assigned values during the construction process
        //          -> post object construction process, it is like a constant (cannot change)

        private string[] names;
        public MemoryManagement(int counter) //Consturctor
        {
            _counter = counter;
            names = new string[MaxSize];
            for (int i = 0; i < MaxSize; i++)
            {
                names[i] = $"This is some long string created to quickly fill up memory {i}";
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\t\tObject{_counter} created ");
            Console.ResetColor();
        }
        ~MemoryManagement() //Destructor
        {
            names = null;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\t\t\t\tObject{_counter} Finalized ");
            Console.ResetColor();
        }

        public void Dispose()
        {
            names = null!;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\t\t\t\t\t\tObject Disposed {_counter}");
            Console.ResetColor();
            GC.SuppressFinalize( this );
            //removes the entry of this object from the finalizer list
        }

        internal static void Test()
        {

            MemoryManagement mm;
            var msg = "Generation of mm is {0}";
            for(int i=0;i<100;i++)
            {
                mm = new MemoryManagement(i);
                if(i>35 && i<75)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(msg, GC.GetGeneration(mm));
                    Console.ResetColor();
                    if(i>40 && i<70)
                    {
                        //Forced collection attempt
                        GC.Collect();
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(msg, GC.GetGeneration(mm));
                        Console.ResetColor();
                        if (i > 50 && i < 60)
                        {
                            //Forced collection attempt
                            GC.Collect();
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine(msg, GC.GetGeneration(mm));
                            Console.ResetColor();
                        }
                    }
                }
                if(i>85 && i<95)
                {
                    mm.Dispose();
                }
            }
            Console.WriteLine("Press a key to terminate");
            Console.ReadKey();

            using(mm = new MemoryManagement(0))
            {
                Console.WriteLine("From withing the using block");
            }
        }

        
    }
}
