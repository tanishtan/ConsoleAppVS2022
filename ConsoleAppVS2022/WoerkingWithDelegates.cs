using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    //Step1: Decleration
    public delegate int ArithematicDelegate(int a, int b);
    internal class WoerkingWithDelegates
    {
        //methods
        static int Add(int x, int y)
        {
            return x + y;
        }
        public int Sub(int x, int y)
        {
            return x - y;
        }
        static int Mul(int x, int y)
        {
            return x * y;
        }


        //void Main()
        //void Main(string[] args)
        //int Main()
        //int Main(string[] args)


        //main method
        internal  static void  Test()
        {
            WoerkingWithDelegates wd = new WoerkingWithDelegates();

            //Step2: Instantiation
            ArithematicDelegate ad = new ArithematicDelegate(Add);
            int a = 10, b = 20, result = 0;
            result = ad(10, 20);
            Console.WriteLine("Test1: Add: "+result);
            a += 10; b += 20;
            //Step3: Invoke
            result=ad.Invoke(a, b);
            Console.WriteLine("Test1: Sub: " + result);

            //Multiple signature to delegate
            //Adding multiple signature to the Delegate
            //List of delegates where each delegare holds one address

            ad += new ArithematicDelegate(wd.Sub); // because it's an instance // ad = (ArithematicDelegate).Combine(ad, new ArithematicDelegate(Sub));
            ad += new ArithematicDelegate(Mul); // because static     // now ad is new object now
            a = 40;b = 40;
            result = ad.Invoke(a, b);
            Console.WriteLine("Test 2: result: " + result);


            //Anonymous method - unnamed method
            ad += delegate (int x, int y)
            {
                return y > 0 ? x / y : 0;
            };
            a = 100; b = 10;
            result = ad.Invoke(a, b);
            Console.WriteLine("Test 4: result: " + result);
            //ad -= new ArithematicDelegate(wd.Sub);

            ad+=(x,y) => x%y;        //-> this is a lambda function which is similar to ad += delegate(int x, int y) {};

            //Expression lambda -> contains a single statement/expression
            //statement lambda -> contanis multiple statements/expression { .. }
            a = 10; b = 7;
            result = ad.Invoke(a, b);
            Console.WriteLine("Test 4: result: " + result);

            // Arguemnets to lambda expression can be passed as follows
            // () -> empty brakets when 0 arguments
            //(a) | a -> with/without one argument
            //(A,b,..N) -> with brackets for more than one argument

            // lambda's are delegate based function

            InvokeDelgatesManually(ad);
        }
        static void InvokeDelgatesManually(ArithematicDelegate arithDel)
        {
            foreach(Delegate del in arithDel.GetInvocationList())
            {
                if (del.Method.Name.StartsWith("M"))
                {
                    object result = del.DynamicInvoke(1000,10);
                    Console.WriteLine($"{del.Method.Name} return {result}");
                }
            }
        }
    }
}
