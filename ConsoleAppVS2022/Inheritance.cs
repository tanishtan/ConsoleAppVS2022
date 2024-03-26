using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    class BaseClass
    {
        private int privateInt;
        protected int protectedInt;
        public int publicInt;
        internal int internalInt;
        protected internal int protetedInternalInt;

        public BaseClass()
        {
            Console.WriteLine("BaseClass.cotr(void) called");
        }

        public BaseClass(int number)
        {
            privateInt = protectedInt = publicInt = internalInt = protetedInternalInt = number;
            Console.WriteLine("BaseClass.cotr(int) called");
        }

        public virtual void show()
        {
            StringBuilder sb = new StringBuilder(); // strings are immutable in .NET
            sb.AppendLine("***** BaseClass.Show() *****").AppendLine($"Private Int: {privateInt}").AppendLine($"Public Int: {publicInt}")
                .AppendLine($"Protected Int: {protectedInt}").AppendLine($"Internal Int: {internalInt}").AppendLine($"Protected Internal Int: {protetedInternalInt}");

            Console.WriteLine(sb.ToString());
            //string s = "Hello";
            //s = s + "World";
        }
    }

    
    /*sealed*/ class Derived : BaseClass
    {
        public Derived() : base() // by default chained to parameterless ctor (even if u don't mention base() it's fine
        {
            Console.WriteLine("DerivedClass.cotr(void) called");
        }
        public Derived(int number) : base(number) // chained to parametrized ctor
        {
            //privateInt = number;
            //protectedInt = publicInt = internalInt = protetedInternalInt = number;
            Console.WriteLine("DerivedClass.cotr(int) called");
        }
        public override void show()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***** Derived.Show() *****")
                //.AppendLine($"Private Int: {privateInt}")
                .AppendLine($"Public Int: {publicInt}")
                .AppendLine($"Protected Int: {protectedInt}")
                .AppendLine($"Internal Int: {internalInt}")
                .AppendLine($"Protected Internal Int: {protetedInternalInt}");
            Console.WriteLine(sb.ToString());
        }
    }

    /*class DerivedofDerivedClass : Derived
    {
        public override void show()
        {
            base.show();
        }
    }*/

    internal class Inheritance 
    {
        internal static void Test()
        {
            BaseClass bc1 = new BaseClass();
            bc1.show();
            BaseClass bc2 = new BaseClass(1234);
            bc2 .show();
            Derived dc1 = new Derived();
            dc1 .show();
            Derived dc2 = new Derived(9876);
            dc2 .show();

            bc1 = dc1; // a variable of baseclass can only point to object of same type i.e. baseclass itself
            bc1 .show();


        }

    }
}
