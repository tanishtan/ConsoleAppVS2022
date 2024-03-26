using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    internal class WorkingWithRecentEvents
    {
        static AutoResetEvent oddEvnet = new AutoResetEvent(false);
        static AutoResetEvent evenEvent = new AutoResetEvent(false);

        static void GeneateOddNumbers() //uses the oddEvent Handler for execution
        {
            for(int i = 1;i<101; i+=2)
            {
                evenEvent.WaitOne();
                Console.Write("{0}\t", i);
                oddEvnet.Set();
            }
        }
        static void GeneateEvenNumbers() // uses the EvenEvent Handler
        {
            for (int i = 0; i < 102; i += 2)
            {
                Console.Write("{0}\t", i);
                evenEvent.Set();
                oddEvnet.WaitOne();
            }
        }
        internal static void Test()
        {
            Thread th1= new Thread(GeneateEvenNumbers);
            Thread th2= new Thread(GeneateOddNumbers);
            th1.Start();
            th2 .Start();
            
            Console.WriteLine("\nAll numbers generated, press akey to terminate");
            Console.ReadKey();
            
            evenEvent.Close();
            oddEvnet.Close();
        }
    }
}
