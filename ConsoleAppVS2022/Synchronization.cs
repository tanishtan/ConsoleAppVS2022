using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading;

namespace ConsoleAppVS2022
{
    internal class Synchronization
    {
        private int counter = 0;
        Dictionary<string, int> priorityCounter = new Dictionary<string, int>();

        public void Run()
        {
            string name = Thread.CurrentThread.Name;
            if(priorityCounter.ContainsKey(name) ) { priorityCounter[name]+=1; }
            else { priorityCounter[name] = 1; }
            Console.WriteLine($"Thread {name} enter Run()");
            while(counter<100)
            {
                int temp = counter;
                temp += 1;
                //Thread.Sleep(1);
                Console.WriteLine($"Thread {name} reports counter at {counter}");
                counter = temp;
            }
            Console.WriteLine($"Thread {name} completes execution. Exiting now");
        }

        public void RunInterlocked() // used for value types, increment the count
        {
            string name = Thread.CurrentThread.Name;
            
            Console.WriteLine($"Thread {name} enter Run()");
            while (counter < 100)
            {
                Interlocked.Increment(ref counter); // decrement, add, exchange
                //int temp = counter;
                //temp += 1;
                //Thread.Sleep(1);
                Console.WriteLine($"Thread {name} reports counter at {counter}");
                //counter = temp;
            }
            Console.WriteLine($"Thread {name} completes execution. Exiting now");
        }

        public static object _syncRoot = new object(); // dummy object - no data in this object
        public void RunMonitor() 
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine($"Thread {name} enter Run()");
            while (counter < 100)
            {
                Monitor.Enter(_syncRoot); // Places lock on the synclock byte
                int temp = counter;
                temp += 1;
                Thread.Sleep(1);
                Console.WriteLine($"Thread {name} reports counter at {counter}");
                counter = temp;
                Monitor.PulseAll(_syncRoot); // notify all waiting threads
                Monitor.Exit(_syncRoot); // Unset the sunclock bit of the synclock buyte of obj
            }
            Console.WriteLine($"Thread {name} completes execution. Exiting now");
        }

        public void RunLock()
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine($"Thread {name} enter Run()");
            while (counter < 100)
            {
                lock(_syncRoot) // Places lock on the synclock byte
                {
                    int temp = counter;
                    temp += 1;
                    Thread.Sleep(1);
                    Console.WriteLine($"Thread {name} reports counter at {counter}");
                    counter = temp;
                }
            }
            Console.WriteLine($"Thread {name} completes execution. Exiting now");
        }

        static Mutex mtx = new Mutex(initiallyOwned: false, "Name", createdNew: out bool createdNew); // first thread can enter
        //createdNew -> a new mutex was created or whether an existing one is obtained
        public void RunMutex()
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"Thread {name} enters {nameof(RunMutex)}()");
            mtx.WaitOne(); //wait to obtain mutex
            Console.WriteLine($"Thread {name} obtains mutex");
            Console.WriteLine("Simulation some activites...");
            Thread.Sleep(1000);
            Console.WriteLine("Simulation more activites...");
            Console.WriteLine($"Thread {name} completes activity. exiting");
            mtx.ReleaseMutex(); // makes mutex available to other threads now
            Console.WriteLine($"Thread {name} exiting");
        }
        static Semaphore sem = new Semaphore(initialCount: 0, maximumCount: 3);
        //initial count -> freely availabe semaphore 0 all threads get blocked
        //maximumCOunt -> how many threads an obtain the semaphore at a point in time

        public void RunSemaphore()
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"Thread {name} enters {nameof(RunSemaphore)}()");
            sem.WaitOne(); //wait to obtain mutex
            Console.WriteLine($"Thread {name} obtains semaphore");
            Console.WriteLine("Simulation some activites...");
            Thread.Sleep(1000);
            Console.WriteLine("Simulation more activites...");
            Console.WriteLine($"Thread {name} completes activity. exiting");
            sem.Release(1); // makes mutex available to other threads now
            Console.WriteLine($"Thread {name} exiting {nameof(RunSemaphore)}()");
        }

        internal static void Test()
        {
            string[] names = { "First", "second", "thrid", "fourth", "fifth" };
            Thread[] myThreads = new Thread[5];
            Synchronization s = new Synchronization();
            //ThreadStart ts1 = s.Run();
            for(int i = 0; i < myThreads.Length; i++)
            {
                //myThreads[i] = new Thread(s.Run);
                myThreads[i] = new Thread(s.RunSemaphore);
                myThreads[i].Name = names[i];
                myThreads[i].Start();
            }
            foreach(var item in s.priorityCounter.Keys)
            {
                Console.WriteLine($"{item} -> {s.priorityCounter[item]}");
            }
            sem.Release(3);
            Console.WriteLine("All threads started, Waiting for completion");
            Console.WriteLine("Press a key to continue");
            Console.ReadKey();
        }
    }
}
