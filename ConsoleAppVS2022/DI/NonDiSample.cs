using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022.DI
{
    public class NonDiSampleClass
    {
        public void Serve()
        {
            Console.WriteLine("NonDISampleClass.Serve() called");
        }
    }
    public class NonDiClientClass
    {
        private NonDiSampleClass _service;
        public NonDiClientClass()
        {
            _service = new NonDiSampleClass();
        }
        public void Start()
        {
            Console.WriteLine("Service started..");
            _service.Serve();
            Console.WriteLine("Service executed...");
        }
        
    }
    public class NonDITestClass
    {
        internal static void Test()
        {
            NonDiClientClass c = new NonDiClientClass();
            c.Start();
        }
    }
}
