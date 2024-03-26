using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class DeveloperAttribute : System.Attribute
    {
        //passing argument / parameters to the attribute
        //1. using constructor - mandatory parameters
        //2. using properties - optional parameters
        private string _name;
        public String name { get { return _name; } set {  _name = value; } }
        public string Description { get; set; } = string.Empty;

        public DeveloperAttribute(String name) // compulsary argument
        {
            _name = name;
        }
        public override string ToString()
        {
            return $"Attribute Details:\n\tName: {_name}\n\tDescription: {Description}";
        }
    }
    [DeveloperAttribute("Virat Kohli", Description = "Cricketer")]
    [Developer("MSD",Description ="Wicket Keeper")]
    internal class WorkingAttributes
    {
        static void ListDevelopers()
        {
            List<object> attributes = typeof(WorkingAttributes).GetCustomAttributes (false).ToList();
            attributes.ForEach (a =>  Console.WriteLine(a));
        }
        internal static void Test()
        {
            ListDevelopers();
        }
    }
}
