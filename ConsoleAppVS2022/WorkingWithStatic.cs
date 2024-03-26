using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    class Statics
    {
        public static int StaticField = 1234; // static (class name ref)
        public int InstanceField = 4321; // non static or instance (object ref)

        static Statics() // no access-specifier, no return type, no arguments
        {
            StaticField = 9876;
            //InstanceField = 1;
            Console.WriteLine("Statics.staticConstructor() called");
        }

        internal Statics() // instance ctor can have specifiers and arguments, no return type
        {
            StaticField = 5678;
            InstanceField = 5678;
            Console.WriteLine("Instance constructor called");
        }
        internal static void StaticShow()
        {
            Console.WriteLine($"Static.StaticShow() returns {StaticField} static fields");
            //Console.WriteLine($"Static.StaticShow() returns {InstanceField} static fields");
        }

        internal void InstanceShow()
        {
            Console.WriteLine($"Static.InstanceShow() returns {StaticField} static fields");
            Console.WriteLine($"Static.InstanceShow() returns {InstanceField} instance fields");
            //StaticShow();
        }

    }
    internal class WorkingWithStatic
    {
        internal static void Test()
        {
            Statics.StaticShow(); // using classname ref to invoke static members
            Statics s = new Statics();
            s.InstanceShow(); // objectname reference to access a member
           /* Statics s2 = new Statics();
            s2.InstanceField = 111;*/
        }
    }
}
