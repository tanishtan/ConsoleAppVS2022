using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    internal class TasksTHread
    {
        internal static async void Test()
        {
            Console.WriteLine("Creating tasks using differnet styles");
            Task t1 = new Task(() =>
            {
                //Thread.Sleep(1000);
                Console.WriteLine("Task t1 executed");
            });
            t1.Start();
            Task t2 = new Task((p) =>
            {
                //Thread.Sleep(500);
                if (p is Product x)
                {
                    x.Show();
                }
                else
                {
                    Console.WriteLine(p);
                }
            }, new Product(1234, "Test",1234,1234));
            t2.Start();
            Task t3 = Task.Run(() =>
            {
                Console.WriteLine("Task 3 started");
            });

            Task t4 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task 4 started");
            });

            Task<int> t5 = new Task<int>((p) =>
            {
                if ((p is Product x))
                {
                    var stockValue = x.UnitPrice * x.UnitInStock;
                    return Convert.ToInt32(stockValue);
                }
                else return 0;
            }, new Product(101, "Apple",10,20));
            t5.Start();
            var value = t5.Result; // call is blocked till the result is retrived
            Console.WriteLine("Value is "+value);

            Func<decimal, decimal, decimal> f2 = (a, b) => a * b;
            //var t6 = Task.FromResult<decimal>(t5.Result); //chaining the task
            Task<decimal> t6 = Task.FromResult<decimal>(f2(10M, 20M));
            //t6.Start();
            //var result = t6.Result;
            var result = t6.GetAwaiter().GetResult();
            Console.WriteLine("Value of t6 is: "+result);

            var chainedTasks = Task.Factory.StartNew<int>(() =>
            {
                Console.WriteLine("T7 started");
                return 600;
            }).ContinueWith<int>(t =>
            {
                var num = t.Result;
                Console.WriteLine("Continued t7 task {0}", num);
                return num * num;
            }).ContinueWith<string>(t =>
            {
                int result = t.Result;
                Console.WriteLine("Continued task2 from t7 with result {0}", result);
                return result.ToString();
            });

            Console.WriteLine("stock is printed, executing wait all statement");
            Task.WaitAll(t1,t2, t3, t4,t5,t6); // Blocking the main thread to ensure that all threads are completed before termination the Main

            var msg = await GetAddressAsync();
            Console.Out.WriteLine(msg);

            Console.WriteLine("Tasks started, waiting for completion");
            //Console.ReadKey();
        }

        static string GetAddress(string input)
        {
            Thread.Sleep(2000);
            if (input == "Microsoft" || input == "Oracle") return $"{input} address found";
            else return $"{input} address not found";
        }

        static async Task<string> GetAddressAsync()
        {
            Console.WriteLine("Enter name");
            string name = Console.ReadLine();
            var t = Task.Factory.StartNew<string>((str) => GetAddress(str?.ToString()), name);
            return t.Result;
        }

        internal static void ParallelTasks()
        {
            // These tasks are scheduled across all the cores.
            Parallel.Invoke(
                () => Console.WriteLine("Circle being drawn"),
                () => Console.WriteLine("Rectangle being drawn"),
                () => Console.WriteLine("Square being drawn"),
                () => Console.WriteLine("Traingle being drawn")
                ); // No predefined order of execution
            Console.WriteLine("Started all drawing, waiting for completion");
            Console.ReadKey();  
        }
    }
}
