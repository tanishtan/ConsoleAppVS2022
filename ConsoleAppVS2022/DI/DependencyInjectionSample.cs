using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022.DI
{
    public interface IDIInterface
    {
        void Serve();
    }
    public class DIImplementationClass : IDIInterface
    {
        public void Serve()
        {
           Console.WriteLine("WithServiceLocatorClass.Serve() called");
            
        }

    }

    public class DiClient
    {
        private IDIInterface _service;
        public DiClient(IDIInterface service)
        {
            _service = service;
        }
        public void Start()
        {
            Console.WriteLine("Service started..");
            _service.Serve();
            Console.WriteLine("Service executed...");
        }
    }
    internal class DependencyInjectionSample
    {
        internal static void Test()
        {
            IDIInterface service = new DIImplementationClass();
            DiClient client = new DiClient(service);
            client.Start();
        }
    }
}
