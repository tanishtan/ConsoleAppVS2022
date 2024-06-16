using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022.DI
{
    public interface IWithServiceLocator
    {
        void Serve();
    }
    public class WithServiceUsingSL : IWithServiceLocator
    {
        public void Serve()
        {
            Console.WriteLine("WithServiceLocatorClass.Serve() called");
        }
        
    }
    public static class ServiceLocator
    {
        public static IWithServiceLocator _service { get; set; }
        public static IWithServiceLocator GetService()
        {
            if (_service == null) _service = new WithServiceUsingSL();
            return _service;
        }
    }
    public class ServiceLocatorClient
    {
        private IWithServiceLocator _service;
        public ServiceLocatorClient()
        {
            _service = ServiceLocator.GetService();
        }
        public void Start()
        {
            Console.WriteLine("Service started..");
            _service.Serve();
            Console.WriteLine("Service executed...");
        }
    }
    internal static class ServiceLocatorTestClass
    {
        internal static void Test()
        {
            ServiceLocatorClient client = new ServiceLocatorClient();
            client.Start();
        }
    }
}
