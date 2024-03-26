using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022.Repositories
{
    // Entity Class
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName}, {LastName}";}
        public DateTime HireDate { get; set; }
    }

    //Context Class
    public class EmployeeDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                connectionString: @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;multipleactiveresultsets=true"
            );
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
