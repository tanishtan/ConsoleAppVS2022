using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    public class Author
    {
        private  int _id;
        private  string _firstName;
        private  string _lastName;
        private  string _city;

        public int Id { get { return _id; } set { _id = value; } }
        public string FirstName { get { return _firstName; } set { _firstName =  value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        public string City { get { return _city; } set { _city = value; } }

        public  void ShowDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Author Details: {_id}").AppendLine($"Name: {_firstName} {_lastName}").AppendLine($"City: {_city}");
            Console.Clear();
            Console.WriteLine( sb.ToString() );
            Console.ReadKey();
        }

        public static explicit operator Author(ArrayList v)
        {
            throw new NotImplementedException();
        }
    }
}
