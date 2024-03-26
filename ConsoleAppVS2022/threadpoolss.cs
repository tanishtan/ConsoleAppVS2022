using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    internal class threadpoolss
    {
        static void TaskToRun(object state) // Parallel processing - threads are background, so once the applicaiton is terminated, thread is destroyed
        {
            if(state == null)
            {
                Console.WriteLine("State is null");
            }
            else
            {
                if(state is Product p)
                {
                    p.Show();
                }
                else
                {
                    Console.WriteLine(state);
                }
            }
            Thread.Sleep(1000);
            Console.WriteLine("TaskToRun() is called and completed");
        }

        internal static void Test()
        {
            ThreadPool.GetMaxThreads(out int workerThread, out int ioThreads);
            Console.WriteLine("Max threads int the clr threadpool {0}, I0: {1}", workerThread, ioThreads);
            
            for(int i = 0; i < 10; i++)
            {
                if (i % 3 == 0)
                    ThreadPool.QueueUserWorkItem(TaskToRun, new Product(i, "Sample", i*324, Convert.ToInt16(i*87)));
                else
                    ThreadPool.QueueUserWorkItem(TaskToRun, i);
            }
            ThreadPool.GetAvailableThreads(out workerThread, out ioThreads);
            Console.WriteLine("Available threads int the clr threadpool {0}, I0: {1}", workerThread, ioThreads);

            Console.WriteLine("Pooled tasks created, waiting for completion");
            Console.ReadKey();
        }
    }
}
