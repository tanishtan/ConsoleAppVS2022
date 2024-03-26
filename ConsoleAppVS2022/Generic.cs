using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    internal class GenericCollection<T> //where T:new()
    {
        List<T> list = new List<T>();
        //T obj = new T();
        //T obj = default(T);
        public void Add(T item)
        {
            list.Add(item);
        }
        public void Remove(T item)
        {
            list.Remove(item);
        }
        public T GetAt(int position)
        {
            return list[position];
        }
        public T[] GetAll()
        {
            return list.ToArray();
        }
        public T this[int index] { get { return list[index]; } set { list[index] = value; } }
        public int Count { get { return list.Count; } }
    }
    public class CollectionManager
    {
        internal static void Test()
        {
            //During instantiation pass the actual parameter
            GenericCollection<int> intColl = new GenericCollection<int>();// new class is creted at runtime
            intColl.Add(1);
            intColl.Add(2);
            int x = intColl.GetAt(0);
            //new class cleed GenericColletion`int

            GenericCollection<string> strColl = new GenericCollection<string>();
            strColl.Add("111");
            strColl.Add("another");
            string s = strColl.GetAt(0);
            //new class cleed GenericColletion`string

            GenericCollection<Product> productColl = new GenericCollection<Product>();
            productColl.Add(new Product(10));
            productColl.Add(new Product(20));
            var p = productColl[0];
            //new class cleed GenericColletion`Product

            int i = 10, j = 20;
            Swap(ref i, ref j);
            double d1 = 10, d2 = 20;
            Swap(ref d1, ref d2);
            Product p1 = new Product(10);
            Product p2 = new Product(20);   
            Swap(ref p1, ref p2);
            string s1 = "s1";
            string s2 = "s2";
            Swap(ref s1, ref s2);
        }

        static void Swap<T>(ref T item1, ref T item2)
        {
            T temp = item1;
            item1 = item2;
            item2=temp;
        }

    }
}
