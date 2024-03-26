using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    public class CustomException : Exception
    {
        public CustomException() : base() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message, Exception innerException) : base(message, innerException) { }
        public string CustomMessage { get; set; } = "";
    }
    
    internal class ExceptionHandling
    {
        static void RaiseCustomException()
        {
            CustomException ex = new CustomException("Custom excpetion raised")
            {
                CustomMessage = "This is a custom message"
            };
            throw ex;
        }
        static void ThrowsException()
        {
            int a = 10,b=0,c=0;
            c = a / b; // divisionbyzeroerror

        }

        static void HandleException()
        {
            try
            {
                ThrowsException();
            }
            catch (DivideByZeroException dbz)
            {
                Console.WriteLine($"ERROR: {dbz.Message}");
                throw;
            }
            catch (ArithmeticException ae)
            {
                Console.WriteLine($"ERROR: {ae.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Finally block run");
            }
            Console.WriteLine("Hello worl");
        }
        internal static void Test()
        {
            //HandleException();
            try
            {
                RaiseCustomException();
            }
            catch(CustomException ce)
            {
                Console.WriteLine($"Error: {ce.Message}");
                Console.WriteLine($"Error: {ce.CustomMessage}");
            }
            finally { Console.WriteLine("custom message execution"); }
        }
    }
}
