using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Delegate functions
namespace ConsoleAppVS2022
{
    delegate void RangeDelegate();
    internal class TestMarks()
    {
        public event RangeDelegate RangeEvent;
        private int _id;
        private int _marks;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int Marks
        {
            get { return _marks; }
            set {
                if (value < 10 || value > 90)
                    RangeEvent?.Invoke();
                _marks = value;
            }
    }
        internal static void Check()
        {
            Console.WriteLine("Enter the id and marks");
            int id = int.Parse(Console.ReadLine());
            int marks = int.Parse(Console.ReadLine());
            TestMarks m = new TestMarks();
            m.Id = id;
            m.RangeEvent += () =>
            {
                Console.WriteLine("Out of range");
            };
            m.Marks = marks;
        }
    }
}
