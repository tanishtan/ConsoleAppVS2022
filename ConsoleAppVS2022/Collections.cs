using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    internal class Collections
    {
        internal static void Test()
        {
            ArrayList list = new ArrayList();
            list.Add(1);
            list.Add("two");
            list.Add(true);
            list.Add(DateTime.Now);
            list.Add(1234.567);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            var item1 = bool.Parse(list[2].ToString());
            Console.WriteLine(item1);

            list.Remove(1);
            foreach(var item in list)
            {
                Console.WriteLine(item);    
            }

            Hashtable ht = new Hashtable();
            ht.Add(1, "one");
            ht.Add(2, "two");
            ht.Add(3, "three");
            ht.Add(0, "zero");
            foreach(var key in ht) {
                Console.WriteLine(key + " - " + ht[key]);
            }
            if (ht.ContainsKey(4)) ht.Add(4,"four"); else ht.Add(5,"five");

            SortedList sl = new SortedList();
            sl.Add(1, "two");
            sl.Add(2,"adf");
            foreach (var key in sl)
            {
                Console.WriteLine(key);
            }
        }

        internal static void TestGeneric()
        {
            List<string> list = new List<string>();
            list.Add("1");
            list.Add("two");
            list.Add("true");
            list.Add(DateTime.Now.ToString());
            list.Add("1234.56");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            var item1 = list[2];
            Console.WriteLine(item1);

            list.Remove("1");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Dictionary<int, string> numberWords = new Dictionary<int, string>()
            {
                { 1, "one" },
                { 2, "two" }
            };
            numberWords.Add(3, "three");
            foreach (var item in numberWords.Keys)
            {
                Console.WriteLine($"{item} - {numberWords[item]}");
            }

            SortedList<int, string> numberWordsSl = new SortedList<int, string>()
            {
                { 1, "one" },
                { 2, "two" }
            };
            numberWordsSl.Add(3, "three");
            foreach (var item in numberWordsSl.Keys)
            {
                Console.WriteLine($"{item} - {numberWordsSl[item]}");
            }

            Stack<int> s1 = new Stack<int>();
            s1.Push(1);
            s1.Push(2);
            //s1.Push("one");
            foreach(var item in s1)
            {
                Console.WriteLine(item);
            }

            Queue<string> s2 = new Queue<string>(); 
            s2.Enqueue("one");

            HashSet<int> set1 = new HashSet<int>() { 1,4,6,9};
            HashSet<int> set2 = new HashSet<int>() { 2,3,5,7 };

            foreach(var item in set1) { Console.WriteLine(item); }
            foreach(var item in set2) { Console.WriteLine(item); }
            HashSet<int> num = new HashSet<int>(set1);
            num.Union(set2);

            foreach(var item in num)
            {
                Console.WriteLine(item);
            }
            num.IntersectWith(set1);

            Action<LinkedList<string>, string> PrintList = (words, testMsg) =>
            {
                Console.WriteLine(testMsg);
                foreach (var word in words)
                {
                    Console.WriteLine(word + " ");
                }
                Console.WriteLine();
            };
            string[] words = { "eurofins", "oracle", "microsoft" };
            LinkedList<string> companies = new LinkedList<string>(words);
            PrintList(companies, "The linked list values");

            companies.AddFirst("google");
            PrintList(companies, "Google added");

            LinkedListNode<string> startnode = companies.First;
            while (startnode != null)
            {
                Console.WriteLine(startnode.Value);
                startnode = startnode.Next; 
            }

        }
    }
}
