using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022.DI
{
    internal interface IServiceA
    {
        void Execute();
    }
    public interface IServiceB 
    {
        void Execute();
    }
    internal class ServiceA : IServiceA
    {
        public void Execute() 
        {
            Console.WriteLine("ServiceA.Execute() executed");
        }
    }
    internal class ServiceB : IServiceB
    {
        public void Execute()
        {
            Console.WriteLine("ServiceB.Execute() executed");
        }
    }
    internal interface IServiceLocator
    {
        void SetService<T>(T service);  
        T GetService<T>();  
    }
    internal class GenericServiceLocator : IServiceLocator
    {
        private readonly Dictionary<object, object> serviceContainer = null;
        public GenericServiceLocator()
        {
            serviceContainer = new Dictionary<object, object>();
            serviceContainer.Add(typeof(IServiceA), new ServiceA());
            serviceContainer.Add(typeof(IServiceB), new ServiceB());
        }
        public void SetService<T>(T service) 
        {
            serviceContainer[service.GetType()] = service;
        }
        public T GetService<T>()
        {
            try
            {
                return (T)serviceContainer[typeof(T)];

            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Service not available");
            }
        }
    }
    public class ServiceAClient
    {
        private IServiceA _serviceA;
        public ServiceAClient()
        {
            _serviceA = new GenericServiceLocator().GetService<IServiceA>();
        }
        public void Start()
        {
            Console.WriteLine("ServiceA started..");
            _serviceA.Execute();
            Console.WriteLine("ServiceA executed...");
        }
    }
    public class ServiceBClient
    {
        private IServiceB _serviceB;
        public  ServiceBClient(IServiceB serviceB) => _serviceB = serviceB;
        public void Start()
        {
            Console.WriteLine("ServiceB started..");
            _serviceB.Execute();
            Console.WriteLine("ServiceB executed...");
        }
    }
    internal static class GenericServiceLocatorSample
    {
        internal static void Test()
        {
            ServiceAClient sac = new ServiceAClient();
            sac.Start();

            IServiceLocator sl = new GenericServiceLocator();

            IServiceB objToInject = sl.GetService<IServiceB>(); 

            ServiceBClient sbc = new ServiceBClient(objToInject);
            sbc.Start();
        }
    }
}
