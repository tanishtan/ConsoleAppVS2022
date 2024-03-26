using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    internal class LinqOperators
    {
        //list initializers
        static List<string> cities = new List<string>
        {
            "Bengaluru","Chennai","Hyderabad","Vizag","Panaji","Mumbai",
            "Trivandrum","Jaipur","Chandigarh","Shimla","Dehradun","Srinagar","Leh","Lucknow",
            "Patna","Raipur","Bhuvaneswar","Kokata","Gantok","Dispur","Itanagar","Aizwal","Imphal",
            "Kohima","Shillong","Bhopal", "Agartala","Ranhi","Gandhinagar","New Delhi","Pondicherry","Kavaratti","Port Blair"
        };

        internal static void Test()
        {
            //BasicQuery();
            //ProjectionOperator();
            //RestrictionOperations();
            //SortingOperator();
            //ElementOperators();
            //AggregationOperators();
            //PartitionOperator();
            //GroupingOperators();
            DataSetsWithLinq();
        }
        static Action<IEnumerable<string>, string> PrintTheList = (list, header) =>
        {
            Console.WriteLine($"*********** {header} ***********");
            Console.WriteLine();
            foreach (var item in list)
            {
                Console.Write($"{item,-20}");
            }
            Console.WriteLine("\n***************************************");
            Console.WriteLine();
        };
        static int counter = 1;
        static void BasicQuery()
        {
            //QUERY SYNTAX
            //interpreter pattern
            var q1 = from item in cities
                     select item;
            //defered query - lazy initialization - query does not hold the data

            //now the query is executed
            
            PrintTheList(q1, $"Test {counter++} Basic Querying with Query syntax");
            

            //METHOD SYNTAX
            var q2 = cities.Select(x => x);
             //now the query is executed
           
            PrintTheList(q1, $"Test {counter++} Basic Querying with Method syntax");
                
        }

        static void ProjectionOperator()
        {
            //select, selectMany, zip
            var q1 = from c in cities
                     select new
                     {
                         StartsWith = c[0],
                         Length = c.Length,
                         Name = c
                     };

            Console.WriteLine($"*********** Test {counter++} ***********");
            Console.WriteLine();
            foreach (var item in q1)
            {
                Console.WriteLine($"Starts With = {item.StartsWith, -2}Length={item.Length:00} Name = {item.Name}");
            }
            Console.WriteLine("\n***************************************");
            Console.WriteLine();

            var q2 = cities.Select(c => new
            {
                StartsWith = c[0],
                Length = c.Length,
                Name = c
            });
            Console.WriteLine($"*********** Test {counter++} ***********");
            Console.WriteLine();
            foreach (var item in q2)
            {
                Console.WriteLine($"Starts With = {item.StartsWith, -2}Length={item.Length:00} Name = {item.Name}");
            }
            Console.WriteLine("\n***************************************");
            Console.WriteLine();

            var numbers = new List<int> { 1,2,3,4,5,6,7,8,9};
            var words = new List<string> { "A", "B", "C", "F" };

            foreach(var zipperitem in numbers.Zip(words))
            {
                Console.WriteLine($"{zipperitem.First} = {zipperitem.Second}");
            }
        }
        static void RestrictionOperations()
        {
            // use of where clause
            var q1 = from c in cities
                     where c.Length > 10
                     select c;

            PrintTheList(q1, $"Test {counter++}");

            var q2 = cities.Where(c => c.Contains("na")).Select(c => c);
            PrintTheList(q2, $"Test {counter++}");

            //complex filters using && and || operator
            var q3 = from c in cities
                     where c.Length>8 || c.StartsWith("B")
                     select c;
            PrintTheList(q3, $"Test {counter++}");

            var q4 = from c in cities
                     orderby c.Length
                     where c.Length>8
                     
                     select new
                     {
                         StartsWith = c[0],
                         Length = c.Length,
                         Name = c
                     };

            Console.WriteLine($"*********** Test {counter++} ***********");
            Console.WriteLine();
            foreach (var item in q4)
            {
                Console.WriteLine($"Starts With = {item.StartsWith,-2}Length={item.Length:00} Name = {item.Name}");
            }
            Console.WriteLine("\n***************************************");
            Console.WriteLine();
        }
        static void SortingOperator()
        {
            // OrderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse
            var q1 = from c in cities
                     orderby c[0] descending, c[1] ascending
                     select c;
            PrintTheList(q1, $"Test {counter++}");
            

            var q2 = cities.OrderBy(c => c[0])
                .ThenByDescending(c => c.Length)
                .Select(c => c);

            PrintTheList(q2, $"Test {counter++}");

            var q3 = cities.Reverse<string>();
            PrintTheList(q3, $"Test {counter++}");
        }
        static void ElementOperators()
        {
            //First, Last, Single, ElementAt
            //FirstOrDefault, LastOrDefault, SingleOrDefault,ElementAtOrDefault

            // cannot be used with declerative query syntax

            var q1 = cities.First();
            var q2 = cities.Last();
            var q3 = cities.First(c => c.Length > 10); // throws exception if not found
            var q4 = cities.Last(c => c.Length < 10);
            Console.WriteLine($"First: {q1}, last: {q2}");
            Console.WriteLine($"First: {q3}, last: {q4}");

            //to avoid exception and return default values in case not found
            var firstMatching = cities.FirstOrDefault(c => c.Length > 120);
            var lastMatching = cities.LastOrDefault(c => c.Length < 10);

            Console.WriteLine($"First: {firstMatching}, last: {lastMatching}");
        }
        static void AggregationOperators()
        {
            //sum, min, max, count, avg
            // Forces Immediate execution
            var sum = cities.Sum(c => c.Length);
            var avg = cities.Average(c => c.Length);
            Console.WriteLine($"Sum: {sum}, avg: {avg}");
        }
        static void PartitionOperator()
        {
            //partitions the dataset into 2 sections
            // Take, Skip, TakeWhile, SKipWhile, Chunk
            var q1 = cities.Take(10);//takes 10 items
            var q2 = cities.Skip(20);//skips 20 and takes remmaing items
            PrintTheList(q1, $"Test {counter++}");
            PrintTheList(q2, $"Test {counter++}");
            var q3 = cities.Skip(5).Take(25).Skip(15).Take(7).Skip(2);
            PrintTheList(q3, $"Test {counter++}");

            //chunk operator -divide the set into smaller sectoins
            var q4 = cities.Chunk(11);
            int i = 1;
            foreach(var q in q4)
            {
                Console.WriteLine($"chunk {i++}");
                foreach(var item in q)
                {
                    Console.Write($"{item,-20}");
                }
                Console.WriteLine();
            }
            //TakeWhile - takes the rows as long as condtion is true, stops when condn is false
            //SKipWhile - skips the rows as long as condn is true, starts when condn is false
            var q5 = cities.TakeWhile(c => c.Length < 10);
            var q6 = cities.SkipWhile(c => c.Length < 10);
            PrintTheList(q5, $"Test Takewhile {counter++}");
            PrintTheList(q6, $"Test SKipwhile {counter++}");
        }
        static void GroupingOperators()
        {
            var q1 = from c in cities
                     orderby c[0]
                     group c by c[0] into item
                     select item;
            Console.WriteLine($"Citeies grouped by first letter");
            foreach(var q in q1)
            {
                Console.WriteLine($"Group {q.Key}");
                q.ToList().ForEach(x => Console.WriteLine($"{x}"));
                Console.WriteLine();
            }
        }
        static string connStr =
   @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;multipleactiveresultsets=true";
        static void DataSetsWithLinq()
        {
            DataSet ds = new DataSet();
            var sqlText1 = "SELECT CategoryId, CategoryName, Description FROM Categories;";

            SqlConnection con = new SqlConnection(connStr);
            con.StateChange += (sender, args) =>
            {
                Console.WriteLine($"State Changed to: {args.CurrentState} from {args.OriginalState}");
            };
            SqlDataAdapter adapter = new SqlDataAdapter(
                 selectCommandText: sqlText1,
                 selectConnection: con);
            adapter.Fill(ds, "Categories");
            var q = from row in ds.Tables["Categories"].AsEnumerable()
                    where row.Field<int>("CategoryId")<5
                    select row;

            foreach(var row in q)
            {
                Console.WriteLine($"{row[0]} and {row[1]}");
            }
        }
    }
}
