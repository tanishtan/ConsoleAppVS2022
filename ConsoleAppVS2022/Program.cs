//using ConsoleAppADO.Repositories;
using ConsoleAppVS2022.DataAccess;
using ConsoleAppVS2022.DI;
using ConsoleAppVS2022.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace ConsoleAppVS2022
{

    public static class Utilities
    {
        //public static bool IsValid(string input) { return input.Contains("@"); } // both above and this stmt are the same
    }
    internal class Program
    {
        static Author author = null;
        static AuthorManager am = new AuthorManager();

        static void TestAuthors()
        {
            Console.Clear();
            Console.WriteLine("**********Author Management******");
            Console.WriteLine("*******1. Create Author****");
            Console.WriteLine("*******2. Update Author****");
            Console.WriteLine("*******3. List All Authors****");
            Console.WriteLine("*******4. Find by id****");
            Console.WriteLine("*******5. Remove Authors ****");
            Console.WriteLine("*******");
            Console.WriteLine("*******0. Quit");
            Console.WriteLine("*********************************");

            Console.Write("Enter choice (number): ");
        }
        static void ListAuthor()
        {
            AuthorManager am = new AuthorManager();
            var authors = am.ListAllAuthors();

            if (!authors.Any())
            {
                Console.WriteLine("No authors found.");
            }
            else
            {
                Console.WriteLine("** Authors List **");
                foreach (var author in authors)
                {
                    author?.ShowDetails();
                    Console.WriteLine("---------------------");
                }
            }

            Console.ReadKey();


        }
        static void CreateAuthor()
        {
            Console.Clear();
            Console.WriteLine("**** Create Author ****");

            Console.Write("Enter Id: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter City: ");
            string city = Console.ReadLine();

            author = am.CreateNewAuthor(id, firstName, lastName, city);

            Console.WriteLine("Author created successfully!");


            author.ShowDetails();
        }
        static void FindId()
        {
            Console.Clear();
            AuthorManager am1 = new AuthorManager();
            Console.WriteLine("Give the id");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var author = am1.FindById(id); // Assign the retrieved object to a variable
                if (author != null) // Check if the author object is not null
                {
                    author.ShowDetails();
                }
                else
                {
                    Console.WriteLine("Author not found with the given ID.");
                }
            }
            Console.ReadKey();
        }


        static void UpdateAuthor(int id)
        {
            Console.Clear();
            Console.WriteLine("Update Called");
            if (author == null)
            {
                Console.WriteLine("There is no author to update. Please create an author first.");
                return;  // Exit the function
            }
            Console.WriteLine("**** Update Author Details: ****");
            /*Console.Write($"Id ({author.Id}): ");
            string StringId = Console.ReadLine();
            Console.Write($"First Name ({author.FirstName}): ");
            string FirstName = Console.ReadLine();
            Console.Write($"Last Name ({author.LastName}): ");
            string LastName = Console.ReadLine();
            Console.Write($"City ({author.City}): ");
            string City = Console.ReadLine();
            author = am.UpdateAuthor(author, StringId, FirstName, LastName, City);*/
            var adf = am.FindById(id);
            if (adf != null)
            {
                am.UpdateeAuthorsNew(adf);
                adf.ShowDetails();
            }
            Console.ReadKey();
        }

        static void RemoveAuthorId(int id)
        {
            Console.Clear();
            am.RemoveAuthorById(id);
        }

        static void Main(string[] args)
        {
            /*Console.WriteLine("Hello, World!");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
            Sample.Test();*/
            //TestProducts();
            //Inheritance.Test();
            // Interfaces.Test();
            //WorkingWithStatic.Test();

            //string email = "some@example.com";
            /*if(Utilities.IsValid(email)) // this works 
            {
                Console.WriteLine("Valid");
            }
            else
            {
                Console.WriteLine("Not valid");
            }*/
            /*if(email.IsValid())
            {
                Console.WriteLine("Valid");
            }
            else
            {
                Console.WriteLine("Not valid");
            }*/
            //MemoryManagement.Test();
            //WoerkingWithDelegates.Test();
            //EventDelegationModel.Test();
            //TestMarks.Check();

            /* int result = 0;
             do
             {
                 TestAuthors(); // Display menu
                 bool validInput = int.TryParse(Console.ReadLine(), out result);
                 if (!validInput)
                 {
                     Console.WriteLine("Invalid input. Please enter a number.");
                     continue;
                 }

                 switch (result)
                 {
                     case 1:
                         CreateAuthor();
                         break;
                     case 2:
                         Console.WriteLine("Enter the id to be updated");
                         int id = int.Parse(Console.ReadLine());
                         UpdateAuthor(id);
                         break;
                     case 3:
                         ListAuthor();
                         break;
                     case 4:
                         FindId();
                         break;
                     case 5:
                         Console.WriteLine("Enter the id to be removed");
                         int.TryParse(Console.ReadLine(), out id);
                         RemoveAuthorId(id);
                         break;
                     case 0:
                         Console.WriteLine("Exiting...");
                         break;
                     default:
                         Console.WriteLine("Invalid choice. Please choose a valid option.");
                         break;
                 }
             } while (result != 0);*/

            /*Collections.Test();*/


            //ExceptionHandling.Test();

            /*ProductManager mgr = new ProductManager();
            mgr.CreateNew(new Product { ProductId = 1, ProductName = "SD", UnitPrice = 0, UnitInStock = 0, IsDiscontinued = false });
            foreach (var item in mgr.GetProduct())
                item.Show();

            CollectionManager.Test();*/

            /*Collections.TestGeneric();

            Action a1 = () => Console.WriteLine("Without arguments");
            Action<int> a2 = (input) => Console.WriteLine("Arguments");
            Action<int, int, string> a3 = (num1, num2, str1) =>
            {
                Console.WriteLine("Action Delegate with int, int ,sting");
                Console.WriteLine($"Arg1 {num1}");
            };
            Action<List<int>> printNum = (list) =>
            {
                foreach (var i in list) { Console.WriteLine(i); }
            };
            a1();
            a2(20);
            a3(1, 2, "add");
            printNum(new List<int>() { 1,2,3,4,5});

            Func<string> f1 = () => { return "Hello"; }; // returns a string
            Func<int, string> f2 = (input) => input.ToString();
            Func<int, int, int> f3 = (num1, num2) => num1 + num2;
            Console.WriteLine(f1());
            Console.WriteLine(f2(290));
            Console.WriteLine(f3(1,2));

            Predicate<int> checkEven = (x) => x % 2 == 0;
            Console.WriteLine(checkEven(4));*/


            //ThreadExample.Test();

            //Synchronization.Test();
            //WorkingWithRecentEvents.Test();


            /*Console.Write("Enter a number (up to 8 digits): ");
            int num = Convert.ToInt32(Console.ReadLine());

            string words = NumWords.ConvertToWords(num);
            Console.WriteLine("The number in words: " + words);*/

            //Booking.Test();

            //FileSystemManipulation.Test();

            //WorkingStreams.Test();

            //WorkingWithADONEt.Test7();

            //threadpoolss.Test();

            //TasksTHread.ParallelTasks();
            //ParallelProgramming.Test();

            //WorkingAttributes.Test();

            //WorkingReflection.Test();

            /*static void TestProducts()
            {
                Product p = new Product();
                p.ProductId = 10;
                p.ProductName = "Eurofins";
                Console.WriteLine(p.ProductId);
                p.Show();
                Console.WriteLine(p.ProductId);*/
            //Object Initializer pattern -compiler object
            /*Product p2 = new Product
            {
                ProductId = 10,
                ProductName = "Sample",
                UnitPrice = 10m,
            };
            p2.Show();
            Product p3 = new Product(999);
            p3.Show();
            var p4 = new Product(999,"Sample");
            p4.Show();
            Product p5 = new Product(999,"Sample",1234);
            p5.Show();
            Product p6 = new Product(999, "Sample", 1234, 776);
            p6.Show();*/
            // WorkingWithADO1.Test();
            //WorkingWithADO1.Test2();
            //WorkingWithADONET.Test3();
            //WorkingWithADONET.Test4();
            //WorkingWithADONET.Test5();
            //WorkingWithADONET.Test6();
            //WorkingWithADONET.Test7();
            //TestGetCustomersById();
            //InsertIntoCustomers();
            //TestGetAllCustomers();

            //WorkingWithADONEt.Test9();

            //WorkingWithDataSet.Test2();

            //LinqOperators.Test();
            //TestEmployeeRepository();

            /*CustomersDbContext db = new CustomersDbContext();

            var q1 = from c in db.Customers
                     where c.Country == "USA"
                     orderby c.CompanyName
                     select c;
            q1.ToList().ForEach(x =>
            {
                Console.WriteLine("Id: {0}, comapny: {1}", x.CustomerId, x.CompanyName);
                Console.WriteLine("COntact: {0}, Locn: {1}-{2}", x.ContactName, x.City, x.Country);
            });*/

            /*//to add a new customer
            Customer custObj = new Customer
            {
                CustomerId = "66778",
                CompanyName = "66778",
                ContactName = "66778",
                City = "66778",
                Country = "66778",
            };
            //db.Customers.Update(custObj);
            //db.Customers.Add(custObj); //this will only add to the client but not server
            //db.SaveChanges(); //write the contents to the db (write to server)
            db.Customers.Remove(custObj);
            db.SaveChanges();*/


            /*var list = db.Customers
                .AsNoTracking()
                .ToList();
            list.ForEach(x =>
            {
                Console.WriteLine("Id: {0}, comapny: {1}", x.CustomerId, x.CompanyName);
                Console.WriteLine("COntact: {0}, Locn: {1}-{2}", x.ContactName, x.City, x.Country);
            });

            //now use linq
            var item = db.Customers
                .AsNoTracking()
                .FirstOrDefault(c => c.CustomerId == "ALFKI");
            if (item is null) { Console.WriteLine("Nothing fetches"); }
            else
            {
                Console.WriteLine("Id: {0}, comapny: {1}", item.CustomerId, item.CompanyName);
                Console.WriteLine("COntact: {0}, Locn: {1}-{2}", item.ContactName, item.City, item.Country);
            }
            var q = db.Customers
                    .FromSql($"select customerid, companyname,contactname, city,country from customers where country='USA'").ToList();

            q.ForEach(x =>
            {
                Console.WriteLine("Id: {0}, comapny: {1}", x.CustomerId, x.CompanyName);
                Console.WriteLine("COntact: {0}, Locn: {1}-{2}", x.ContactName, x.City, x.Country);
            });

            var count = db.Database.SqlQuery<int>($"select count(*) from products").ToList().First();
            Console.WriteLine("count: {0}", count);

            q = db.Customers
                    .FromSql($"exec sp_getAllCustomers 'na'").ToList();

            q.ForEach(x =>
            {
                Console.WriteLine("Id: {0}, comapny: {1}", x.CustomerId, x.CompanyName);
                Console.WriteLine("COntact: {0}, Locn: {1}-{2}", x.ContactName, x.City, x.Country);
            });*/
            //TestEmployeeRepository();
            /*            NonDITestClass.Test();
                        Console.WriteLine("Service locator");
                        ServiceLocatorTestClass.Test();
                        Console.WriteLine("\n\ndependency inection sample");
                        DependencyInjectionSample.Test();
                        Console.WriteLine("\n\ngeneric sample");
                        GenericServiceLocatorSample.Test();*/

            /*var services = new ServiceCollection().AddScoped<IServiceA,ServiceA>().AddScoped<IServiceB, ServiceB>();

            IServiceProvider provider = services.BuildServiceProvider();

            var serviceBObj = provider.GetService<IServiceB>();
            ServiceBClient sac = new ServiceBClient(serviceBObj);

            sac.Start();*/

            /*HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddScoped<IServiceA, ServiceA>();
            builder.Services.AddScoped<IServiceB, ServiceB>();
            

            IHost host = builder.Build();
            RunApp(host.Services);
            host.Run();*/

            WorkingWithWebApi.test();
        }

        static void RunApp(IServiceProvider provider)
        {
            //var obj = provider.GetService<IServiceB>();
            ServiceBClient serviceBClient = (ServiceBClient)Activator.CreateInstance(typeof(ServiceBClient), provider.GetService<IServiceB>());
            serviceBClient.Start();
        }

        static void TestEmployeeRepository()
        {
            Action<Employee> PrintDetails = (c) => Console.WriteLine($"{c.EmployeeId} {c.FullName}");
            IRepository<Employee, int> repository = new EmployeeRepository();
            var items = repository.GetAll();
            items.ToList().ForEach(c => PrintDetails(c));
            var emp = repository.FindById(1);
            PrintDetails(emp);
            emp = new Employee
            {
                EmployeeId = 0,
                FirstName = "Harry",
                LastName = "Kane",
                HireDate = DateTime.Now,
            };
            repository.Upsert(emp);
            //refetch to check the insert
            items.ToList().ForEach((c) => PrintDetails(c));
            Console.WriteLine("Updating now");
            emp = repository.FindById(10);
            emp.FirstName = "Hardik";
            emp.HireDate = new DateTime(2020, 02, 02);
            repository.Upsert(emp);

            //refetch the row to check the update operation
            emp = repository.FindById(10);
            PrintDetails(emp);
            Console.WriteLine("Deleting now");
            //remove the row we have inserted

            repository.RemoveById(10);
        }
        /*
        static void TestGetAllCustomers()
        {
            CustomerDataAccess cda = new CustomerDataAccess();
            var list = cda.GetAllCustomers();
            list.ForEach(c =>
            {
                Console.WriteLine("ID: {0}, Company: {1}", c.CustomerId, c.CompanyName);
                Console.WriteLine("\tContact: {0}, Location: {1}-{2}", c.ContactName, c.City, c.Country);
            });
            Console.WriteLine();
        }

        static void TestGetCustomersById()
        {
            try
            {

                CustomerDataAccess cda = new CustomerDataAccess();
                string Id = Console.ReadLine();
                var list = cda.GetCustomersById(Id);
                if (list is null)
                {
                    Console.WriteLine("Nothing Found");
                }
                else
                {
                    Console.WriteLine("ID: {0}, Company: {1}", list.CustomerId, list.CompanyName);
                    Console.WriteLine("\tContact: {0}, Location: {1}-{2}", list.ContactName, list.City, list.Country);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            Console.WriteLine();
        }*/

        /*
        static void InsertIntoCustomers()
        {
            CustomerDataAccess cda = new CustomerDataAccess();

            Func<string, string> GetInput = (text) =>
            {
                Console.WriteLine($"Enter {text}:");
                var str = Console.ReadLine();
                return str;
            };
            /*Customer cust = new Customer
            {
                CustomerId="ABCDE",
                CompanyName="ADBCDE",
                ContactName="ABDCDE",
                City="ADBCCC",
                Country="asrfll"
            };*/ // --one way
        /*Customer cust = new Customer
        {
            CustomerId = GetInput("Customer Id"),
            CompanyName = GetInput("Company Name"),
            ContactName = GetInput("Contact Name"),
            City = GetInput("City"),
            Country = GetInput("Country")
        };
        cda.InsertIntoCustomers(cust);//second-wy
    }

    static void UpdateCustomers()
    {
        CustomerDataAccess cda = new CustomerDataAccess();

        Func<string, string> GetInput = (text) =>
        {
            Console.WriteLine($"Enter {text}:");
            var str = Console.ReadLine();
            return str;
        };
        *//*Customer cust = new Customer
        {
            CustomerId="ABCDE",
            CompanyName="ADBCDE",
            ContactName="ABDCDE",
            City="ADBCCC",
            Country="asrfll"
        };*//* // --one way
        Customer cust = new Customer
        {
            CustomerId = GetInput("Customer Id"),
            CompanyName = GetInput("Company Name"),
            ContactName = GetInput("Contact Name"),
            City = GetInput("City"),
            Country = GetInput("Country")
        };
        cda.UpadteNewCustomer(cust);//second-wy
    }


    static void TestGetAllCustomers()
    {
        CustomerDataAccess cda = new CustomerDataAccess();
        var list = cda.GetAllCustomers();
        list.ForEach(c =>
        {
            Console.WriteLine("ID: {0}, Company: {1}", c.CustomerId, c.CompanyName);
            Console.WriteLine("\tContact: {0}, Location: {1}-{2}", c.ContactName, c.City, c.Country);
        });
        Console.WriteLine();
    }

    static void TestGetCustomersById()
    {
        try
        {

            CustomerDataAccess cda = new CustomerDataAccess();
            string Id = Console.ReadLine();
            var list = cda.GetCustomersById(Id);
            if (list is null)
            {
                Console.WriteLine("Nothing Found");
            }
            else
            {
                Console.WriteLine("ID: {0}, Company: {1}", list.CustomerId, list.CompanyName);
                Console.WriteLine("\tContact: {0}, Location: {1}-{2}", list.ContactName, list.City, list.Country);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }



        Console.WriteLine();
    }

    static void InsertIntoCustomers()
    {
        CustomerDataAccess cda = new CustomerDataAccess();

        Func<string, string> GetInput = (text) =>
        {
            Console.WriteLine($"Enter {text}:");
            var str = Console.ReadLine();
            return str;
        };
        *//*Customer cust = new Customer
        {
            CustomerId="ABCDE",
            CompanyName="ADBCDE",
            ContactName="ABDCDE",
            City="ADBCCC",
            Country="asrfll"
        };*//* // --one way
        Customer cust = new Customer
        {
            CustomerId = GetInput("Customer Id"),
            CompanyName = GetInput("Company Name"),
            ContactName = GetInput("Contact Name"),
            City = GetInput("City"),
            Country = GetInput("Country")
        };
        cda.InsertIntoCustomers(cust);//second-wy
    }

    static void UpdateCustomers()
    {
        CustomerDataAccess cda = new CustomerDataAccess();

        Func<string, string> GetInput = (text) =>
        {
            Console.WriteLine($"Enter {text}:");
            var str = Console.ReadLine();
            return str;
        };
        *//*Customer cust = new Customer
        {
            CustomerId="ABCDE",
            CompanyName="ADBCDE",
            ContactName="ABDCDE",
            City="ADBCCC",
            Country="asrfll"
        };*//* // --one way
        Customer cust = new Customer
        {
            CustomerId = GetInput("Customer Id"),
            CompanyName = GetInput("Company Name"),
            ContactName = GetInput("Contact Name"),
            City = GetInput("City"),
            Country = GetInput("Country")
        };
        cda.UpadteNewCustomer(cust);//second-wy
    }

}*/
    }
}
