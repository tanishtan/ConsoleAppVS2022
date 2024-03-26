using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppVS2022.DataAccess
{
    internal class CustomersDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        //property name match the table name ans it should be plural
        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // use the sqlserver .net data provider class
            optionsBuilder.UseSqlServer(
                connectionString: @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;multipleactiveresultsets=true"
            );

            //creatting a logger for inspecting generated sql
            optionsBuilder.LogTo(Console.Out.WriteLine, minimumLevel: Microsoft.Extensions.Logging.LogLevel.Trace);
        }
    }
}
