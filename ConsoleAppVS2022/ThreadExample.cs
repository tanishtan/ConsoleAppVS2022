using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleAppVS2022
{
    internal class ThreadExample
    {
        internal static void Test()
        {
            ThreadStart ts1 = new ThreadStart(Run);
            ParameterizedThreadStart ps1 = new ParameterizedThreadStart(RunObject);
            Thread th1 = new Thread(ts1); // Managed thread
            Thread th2 = new Thread(Run);
            Thread th3 = new Thread(()=>Run());
            Thread th4 = new Thread(ps1);
            Console.WriteLine("All threads create. Press akey to start");
            Console.ReadKey();

            //name the thread
            th1.Name = "First"; th2.Name = "Second"; th3.Name = "Third"; th4.Name = "Fourth"; // Os creates these threads

            th1.Priority = ThreadPriority.Highest;
            

            Product p = new Product(101, "Thread Product name", 1234M, 999);

            //start the thread
            th1.Start();
            th2.Start();
            th3.Start();
            th4.Start(p);
            Console.WriteLine("All threads started. Executing subsequent tasks");
            Console.ReadKey();
            Console.WriteLine("Task1");
            Console.WriteLine("Task 2");
            Console.WriteLine("Press a key to terminate");

        }

        static void Run()
        {
            // Access CurrentThread from System.Threading class
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"The {name} is currently executing");
            Thread.Sleep(millisecondsTimeout: 1000);
            Console.WriteLine($"RunThread Thread {name} resumed execution. About to terminate");
            Console.WriteLine($"RunThread Thread {name} exited");
        }

        static void RunObject(object state)
        {
            // Access CurrentThread from System.Threading class
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"The {name} is currently executing");

            if (state != null)
            {
                if (state is Product p)
                {
                    p.Show();
                }
            }

            Thread.Sleep(millisecondsTimeout: 1000);
            Console.WriteLine($"RunObject Thread {name} resumed execution. About to terminate");
            Console.WriteLine($"RunObject Thread {name} exited");
        }
    }
}
