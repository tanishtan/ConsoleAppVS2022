using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{

    public interface IBase
    {
        void DoWork();
    }
    public interface IChild : IBase 
    {
        void PerformTask();
        string Name { get; set; }
    }
    public interface ITransact
    {
        void PerformTask();
        void DoWork2()
        {
            Console.WriteLine("ITransact.DoWork called");
        }
    }

    public class InterfaceImplementation : IChild,IBase,ITransact
    {
        public void DoWork() { Console.WriteLine("InterfaceImplemtation.DoWork() called"); }
        public void PerformTask() 
        {
            Console.WriteLine("InterfaceImplemtation.PerformTask() called"); 
        }
        public string Name { get; set; }

        //Explicit implementation of interface (To distinguish where the perform task is being called from)
        void IChild.PerformTask()
        {
            Console.WriteLine("IChild.PerformTask()");
        }
        void ITransact.PerformTask()
        {
            Console.WriteLine("ITransact.PerformTask()");
        }
    }

    internal class Interfaces
    {
        internal static void Test()
        {
            InterfaceImplementation ii = new InterfaceImplementation();
            ii.DoWork();
            ii.PerformTask();
            ii.Name = "Test";
            Console.WriteLine("ii.Name: {0}", ii.Name);

            //Dynamic Binding
            IBase ib = ii; // ib is called as the interface pointer
            ib.DoWork();
            
            IChild ic = ii;
            ic.DoWork();
            ic.PerformTask();
            ic.Name = "Test";
            Console.WriteLine(ic.Name);
            ITransact it = ii;
            it.PerformTask();

            //Take the example of net banking and mobile banking where only certain features of net banking are available in the mobile banking.
        }
    }
}
