using System;
using System.Data.SqlClient;

namespace ConsoleApp_ADO.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ADO.NET SQL Console App");
            string connectionStr = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using(SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT TOP 10 * FROM Products", connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("ID".PadRight(5) + "Brand".PadRight(35) + "Items");
                        Console.WriteLine("----------------------------------------------------------");

                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0].ToString().PadRight(5) + reader[1].ToString().PadRight(35) + reader[4].ToString().PadRight(35));
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}