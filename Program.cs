using System;
using System.Data.SqlClient;

namespace ConsoleApp_ADO.Net
{
    class Program
    {
        public static string DatabaseName { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("ADO.NET SQL Console App");
            Console.Write("- Enter Database Name (or Press Enter to use default): ");

            string dbName = Console.ReadLine();
            if (dbName.Trim() == ""){
                DatabaseName = "Northwind";
                Console.WriteLine("No Database selected, Using :'Northwind' as Default");
            } else {
                DatabaseName = dbName;
                Console.WriteLine($"Connecting to Database: '{DatabaseName}'.\nPlease wait...");
            }

            string connectionStr = $@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog={DatabaseName};Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionStr))
                {
                    connection.Open();
                    int readTableRows = 15;
                    using (SqlCommand command = new SqlCommand($"SELECT TOP {readTableRows} * FROM Products", connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("ID".PadRight(5) + "Brand".PadRight(35) + "Items");
                            Console.WriteLine("----------------------------------------------------------");

                            while(reader.Read()){
                                Console.WriteLine(reader[0].ToString().PadRight(5) + reader[1].ToString().PadRight(35) + reader[4].ToString().PadRight(35));
                            }
                        }
                    }

                    // When applying 'Using' with SqlConnection/SqlCommand
                    // We dont neen to use .Close/Dispose
                    // Close and Dispose Connection
                    // connection.Close();
                    // connection.Dispose();
                }
            }
            catch (Exception e){
                // Connection Exception
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection to database failed...");
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
            finally{
                Console.ReadLine();
            }
        }
    }
}