using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022
{
    public class AuthorManager
    {
        static Author[] authorObject = new Author[10];
        static int currentIndex = 0;

        static ArrayList list = new ArrayList();
        public Author CreateNewAuthor(int Id, string FirstName, string LastName, string City)
        {
            Author author = new Author();
            author.Id = Id;
            author.FirstName = FirstName;
            author.LastName = LastName;
            author.City = City;
            if (currentIndex == authorObject.Length)
            {
                Author[] newAuthorObject = new Author[currentIndex + 10];

                Array.Copy(authorObject, newAuthorObject, authorObject.Length);
                authorObject = newAuthorObject;
            }
            authorObject[currentIndex++] = author;
            return author;
        }
        public Author UpdateAuthor(Author author, string StringId, string FirstName, string LastName, string City)
        {
            if (StringId.Length > 0)
                author.Id = int.Parse(StringId);
            if (FirstName.Length > 0)
                author.FirstName = FirstName;
            if (LastName.Length > 0)
                author.LastName = LastName;
            if (City.Length > 0)
                author.City = City;
            return author;
        }
        public Author[] ListAllAuthors()
        {
            return authorObject;
        }
        /*public Author[] ListAllAuthors()
        {
            Author[] arr2 = new Author[authorObject.Count()];
            list.Add(arr2);
            return (Author) list.ToArray();
        }*/
        public Author FindById(int id)
        {
            if (authorObject.Length == 0)
            {
                return null; // Return null if the array is empty
            }

            foreach (var ids in authorObject)
            {
                if (ids.Id == id)
                {
                    return authorObject[id - 1];
                }
            }
            return null; // Return null if no author with the given ID is found
        }


        public void UpdateeAuthorsNew(Author author)
        {
            if(FindById(author.Id) != null)
            {
                Console.WriteLine("Firstname: ");
                string fname = Console.ReadLine();
                author.FirstName = string.IsNullOrEmpty(fname) ?  author.FirstName : fname ;
                Console.WriteLine("Lastname: ");
                string lname = Console.ReadLine();
                author.LastName = string.IsNullOrEmpty(lname) ?  author.LastName : lname;
                Console.WriteLine("City: ");
                string city = Console.ReadLine();
                author.City = string.IsNullOrEmpty(city) ? author.City : city;
            }
        }

        public void RemoveAuthorById(int id)
        {
            Author authorToRemove = FindById(id);
            if (authorToRemove.Id!=id) { Console.WriteLine("Not found");return; }
            
            if (authorToRemove != null)
            {
                int removalIndex = Array.IndexOf(authorObject, authorToRemove);

                // Shift elements to the left to fill the gap
                for (int i = removalIndex + 1; i < authorObject.Length; i++)
                {
                    authorObject[i - 1] = authorObject[i];
                }

                currentIndex--; // Decrement the current index

                // Shrink the array to remove the empty element
                Array.Resize(ref authorObject, currentIndex);
            }

        }

           
    }
        
 }


