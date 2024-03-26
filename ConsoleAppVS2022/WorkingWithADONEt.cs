using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
    
using System.Transactions;
using System.Data.SqlClient;

namespace ConsoleAppVS2022
{
    internal class WorkingWithADONEt
    {
        internal static void Test()
        {
            //define the connection strin
            var connStr = @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true";
            var sqlText = "SELECT CustomerId, CompanyName, ContactName, City, Country From Customers";
            Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command = new Microsoft.Data.SqlClient.SqlCommand();
            command.CommandText = sqlText;
            command.CommandType = CommandType.Text;
            command.Connection = connection;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Id: {0}, Company: {1}\n\tContact: {2}\n\tLocation: {3}-{4}",
                    reader.GetString(0), reader["CompanyName"].ToString(),
                    reader[2].ToString(),reader.GetString(3),reader.GetString(4));
            }
            reader.Close();

            connection.Close();
        }
        //Extract ProductId int, Productname string, unitprice decimal, unitsinstock short, isdiscontinued bool from products and print it
        internal static void Product()
        {
            var connStr = @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true";
            var sqlText = "SELECT ProductId, ProductName, UnitPrice, UnitsInStock, Discontinued From Products";
            Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command = new Microsoft.Data.SqlClient.SqlCommand();
            command.CommandText = sqlText;
            command.CommandType = CommandType.Text;
            command.Connection = connection;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Id: {0}, Name: {1}\n\tPrice: {2}\tStock: {3}\tDiscontinued: {4}",
                    reader.GetInt32(0), reader["ProductName"].ToString(),
                    reader.GetDecimal(2), reader.GetInt16(3), reader.GetBoolean(4));
            }
            reader.Close();

            connection.Close();
        }
        internal static void Test3()
        {
            var connStr = @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;multipleactiveresultsets=true";
            var sqlText = "SELECT CategoryId, CategoryName, Description FROM Categories; " +
                "SELECT ProductId, ProductName, UnitPrice, UnitsInStock, Discontinued From Products; ";
            Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command = new Microsoft.Data.SqlClient.SqlCommand();
            command.CommandText = sqlText;
            command.CommandType = CommandType.Text;
            command.Connection = connection;

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("Id: {0}, Name: {1}, Description: {2}",
                    reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
            }
            reader.NextResult();

            while (reader.Read())
            {
                Console.WriteLine("Id: {0}, Name: {1}\n\tPrice: {2}\tStock: {3}\tDiscontinued: {4}",
                    reader.GetInt32(0), reader["ProductName"].ToString(),
                    reader.GetDecimal(2), reader.GetInt16(3), reader.GetBoolean(4));
            }
            reader.Close();

            connection.Close();
        }
        internal static void Test4()
        {
            //define the connection string 
            var connStr =
            @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;multipleactiveresultsets=true";
            //var connStr = @"Server=(local)\MSSQLSERVER2;database=northwind;integrated security=sspi";
            var sqlText1 = "SELECT CategoryId, CategoryName, Description FROM Categories;";

            Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command1 = new Microsoft.Data.SqlClient.SqlCommand();
            command1.CommandText = sqlText1;
            command1.CommandType = CommandType.Text;
            command1.Connection = connection;
            var reader1 = command1.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("".PadLeft(80, '-'))
                .AppendFormat("{0,-5}{1,-40}{2}\n", "Id", "Name", "Description")
                .AppendLine("".PadLeft(80, '-'));
            while (reader1.Read())
            {
                sb.AppendFormat("{0,-5}{1,-40}{2}\n", reader1.GetInt32(0), reader1.GetString(1), reader1.GetString(2));
                var sqlText2 = " SELECT ProductId, ProductName, UnitPrice, UnitsinStock,Discontinued FROM Products " +
                " WHERE CategoryId = " + reader1["CategoryId"].ToString();
                var command2 = new Microsoft.Data.SqlClient.SqlCommand();
                command2.CommandText = sqlText2;
                command2.CommandType = CommandType.Text;
                command2.Connection = connection;
                var reader2 = command2.ExecuteReader();
                sb.AppendLine("".PadLeft(80, '-'))
                .AppendFormat("{0,-5}{1,-40}{2,-10}{3,-10},{4}\n", "Id", "Name", "Price", "Stock", "Discontinued")
                .AppendLine("".PadLeft(80, '-'));
                while (reader2.Read())
                {
                    sb.AppendFormat("{0,-5}", reader2.GetInt32(0))
                        .AppendFormat("{0,-40}", reader2.GetString(1))
                        .AppendFormat("{0,-10}", reader2.GetDecimal(2))
                        .AppendFormat("{0,-10}", reader2.GetInt16(3))
                        .AppendFormat("{0}", reader2.GetBoolean(4))
                        .Append("\n");
                }
                if (!reader2.IsClosed) reader2.Close();

                sb.AppendLine("\n".PadLeft(80, '='));
            }
            Console.WriteLine(sb.ToString());

            if (!reader1.IsClosed) reader1.Close();
            if (connection != null)
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
        }
        internal static void Test5()
        {
            var connStr =
            @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;multipleactiveresultsets=true";
            Console.Clear();
            Console.WriteLine("Enter category id ");
            string id = Console.ReadLine();
            var sqlText1 = "select categoryid, categoryname, description from categories " +
                 //" where categoryId = " + id;
                 " where categoryId = @categoryId";
            Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command1 = new Microsoft.Data.SqlClient.SqlCommand();
            command1.CommandText = sqlText1;
            command1.CommandType = CommandType.Text;
            command1.Connection = connection;

            // to prevent SQL Injection
            Microsoft.Data.SqlClient.SqlParameter p1 = new Microsoft.Data.SqlClient.SqlParameter();
            p1.ParameterName = "@categoryId";
            p1.SqlDbType = SqlDbType.Int;
            p1.Size = 4;
            p1.Direction = ParameterDirection.Input;
            p1.Value = id;

            command1.Parameters.Add(p1);

            var reader1 = command1.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("".PadLeft(80, '-'))
                .AppendFormat("{0,-5}{1,-40}{2}\n", "Id", "Name", "Description")
                .AppendLine("".PadLeft(80, '-'));
            while (reader1.Read())
            {
                sb.AppendFormat("{0,-5}{1,-40}{2}\n", reader1.GetInt32(0), reader1.GetString(1), reader1.GetString(2));
            }
            Console.WriteLine(sb.ToString());

            if (!reader1.IsClosed) reader1.Close();
            if (connection != null)
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
        }
        static string connStr =
          @"Server=(local);database=northwind;integrated security=sspi;trustservercertificate=true;multipleactiveresultsets=true";
        internal static void Test6()
        {
            var sqlText1 = "INSERT INTO Customers (CustomerID, Companyname, ContactName, City, Country) " +
                " VALUES (@custId, @company, @contact, @city, @country) ";

            string id, company, contact, city, country;
            Console.Write("Enter ID: ");
            id = Console.ReadLine();
            Console.Write("Enter Company: ");
            company = Console.ReadLine();
            Console.Write("Enter Contact: ");
            contact = Console.ReadLine();
            Console.Write("Enter City: ");
            city = Console.ReadLine();
            Console.Write("Enter Country: ");
            country = Console.ReadLine();

            Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command1 = new Microsoft.Data.SqlClient.SqlCommand();
            command1.CommandText = sqlText1;
            command1.CommandType = CommandType.Text;
            command1.Connection = connection;

            Microsoft.Data.SqlClient.SqlParameter p1 = new Microsoft.Data.SqlClient.SqlParameter();
            p1.ParameterName = "@custId";
            p1.Size = 5;
            p1.SqlDbType = SqlDbType.VarChar;
            p1.Value = id;
            command1.Parameters.Add(p1);

            Microsoft.Data.SqlClient.SqlParameter p2 = new Microsoft.Data.SqlClient.SqlParameter("@company", SqlDbType.VarChar, 50);
            p2.Value = company;
            command1.Parameters.Add(p2);

            Microsoft.Data.SqlClient.SqlParameter p3 = new Microsoft.Data.SqlClient.SqlParameter("@contact", contact);
            command1.Parameters.Add(p3);

            command1.Parameters.AddWithValue("@city", city);
            command1.Parameters.AddWithValue("@country", country);
            try
            {
                command1.ExecuteNonQuery();
                Console.WriteLine("Row inserted into the table.");
            }
            catch (Microsoft.Data.SqlClient.SqlException sqle)
            {
                foreach (Microsoft.Data.SqlClient.SqlError error in sqle.Errors)
                { Console.WriteLine(error.Message); }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        internal static void Test7()
        {
            var sqlText1 = "sp_InsertCustomer"; // change

            string id, company, contact, city, country;
            Console.Write("Enter ID: ");
            id = Console.ReadLine();
            Console.Write("Enter Company: ");
            company = Console.ReadLine();
            Console.Write("Enter Contact: ");
            contact = Console.ReadLine();
            Console.Write("Enter City: ");
            city = Console.ReadLine();
            Console.Write("Enter Country: ");
            country = Console.ReadLine();

            Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection();
            connection.ConnectionString = connStr;
            connection.Open();

            var command1 = new Microsoft.Data.SqlClient.SqlCommand();
            command1.CommandText = sqlText1;
            command1.CommandType = CommandType.StoredProcedure; // change
            command1.Connection = connection;

            //change
            command1.Parameters.AddWithValue("@custmerId", id);
            command1.Parameters.AddWithValue("@company", company);
            command1.Parameters.AddWithValue("@contact", contact);
            command1.Parameters.AddWithValue("@city", city);
            command1.Parameters.AddWithValue("@country", country);
            try
            {
                command1.ExecuteNonQuery();
                Console.WriteLine("Row inserted into the table.");
            }
            catch (Microsoft.Data.SqlClient.SqlException sqle)
            {
                foreach (Microsoft.Data.SqlClient.SqlError error in sqle.Errors)
                { Console.WriteLine(error.Message); }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        internal static void Test8()
        {
            Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(connStr); 

            //Open the connection and create a transaction
            connection.Open();
            var trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);
            var sql1 = "INSERT INTO Customers(CustomerId, CompanyName, ContactName, City, Country) " +
                " values('VWXYZ','transacted company','transact contact','transact','tranasct'); ";
            var sql2 = "DELETE FROM Customers where Customerid = '12345'; "; // Foreign key constraint (for vlfki)
            //2 commands using the same connection
            var cmd1= new Microsoft.Data.SqlClient.SqlCommand(sql1, connection, trans);
            var cmd2 = new Microsoft.Data.SqlClient.SqlCommand(sql2, connection, trans);
            //execute nonquery on both the commands within a try/catch block
            try
            {
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                trans.Commit();
            }catch (Exception ex)
            {
                Console.WriteLine("Transaction Failed"+ex.Message);
                trans.Rollback();
            }
            finally
            {
                connection.Close();
            }
            //commit if things can complete, rollback on exceptions
            // if i comment the trans part, then it is defualt ReadCommitted and any one transaction passes
            // then the particular operation is done
        }

        internal static void Test9()
        {
            //For Distributed transactions, use the System.Transactions namespace

            //Scope - Required - if there is an existing transaction, take part in it, if not create a new transaction 
            //      - RequiresNew - always create a new transaction 
            //      - Suppressed - do not take part in any transaction, even if it exists. 
            TransactionScopeOption options = TransactionScopeOption.Required;


            //Isolation Levels - ReadUncommitted, ReadCommitted, RepeatableRead, Serializable, Snapshot, Chaos
            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.Serializable;

            TransactionManager.ImplicitDistributedTransactions = true;
            //This scope objects enlists the connections into the DTC - MSDTC 
            using (System.Transactions.TransactionScope scope
                = new System.Transactions.TransactionScope(options, transactionOptions))
            {
                Microsoft.Data.SqlClient.SqlConnection con1 = new Microsoft.Data.SqlClient.SqlConnection(connStr);
                Microsoft.Data.SqlClient.SqlConnection con2 = new Microsoft.Data.SqlClient.SqlConnection(connStr);
                var sql1 = "INSERT INTO Customers(CustomerId, CompanyName, ContactName, City, Country) " +
               " VALUES ('54321','transacted company', 'transact contact', 'transact', 'transact'); ";
                var sql2 = "DELETE FROM Customers WHERE CustomerId='99'; "; //Foreign Key constraint

                var cmd1 = new Microsoft.Data.SqlClient.SqlCommand(sql1, con1);
                var cmd2 = new Microsoft.Data.SqlClient.SqlCommand(sql2, con2);
                con1.Open();
                con2.Open();
                cmd1.ExecuteNonQuery();
                    // check whether it can be executed
                cmd2.ExecuteNonQuery();
                // check whether it can be executed
                // if both say yes, then complete() will instruct both db to commit
                // if both say no, then complete() will instruct both db to rollback
                scope.Complete(); //Either it gets committed or rolled back. 
                con1.Close();
                con2.Close();

                // check the DTC in component services from windows start menu
            }

        }
    }
}
