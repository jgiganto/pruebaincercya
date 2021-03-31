using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;

namespace Customers
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CustomersModel> clientes = new List<CustomersModel>();

            string path = @"C:\Users\je\Desktop\pruebaInercya\pruebaincercya\Customers\Customers\Customers.csv";
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", Encoding = Encoding.GetEncoding("ISO-8859-1") };
                using (var reader = new StreamReader(path, Encoding.GetEncoding("ISO-8859-1")))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<CustomersModel>();

                    List<CustomersModel> productos = new List<CustomersModel>();
                    clientes = productos;
                    foreach (var r in records)
                    {
                        productos.Add(new CustomersModel
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Address = r.Address,
                            City = r.City,
                            Country = r.Country,
                            PostalCode = r.PostalCode,
                            Phone = r.Phone
                        });
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }


            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "DESKTOP-64C2FJI";
                builder.IntegratedSecurity = true;
                //builder.Password = "<your_password>";
                builder.InitialCatalog = "TiendaGames";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    //String sql = "select CategoryName from Categories";
                    String sql = @"INSERT INTO Customers2
                                   (Id
                                   ,Name
                                   ,Address
                                   ,City
                                   ,Country
                                   ,PostalCode
                                   ,Phone)
                             VALUES
                                   (@Id
                                   ,@Name
                                   ,@Address
                                   ,@City
                                   ,@Country
                                   ,@PostalCode
                                   ,@Phone)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("@Id",SqlDbType.VarChar);
                        command.Parameters.Add("@Name", SqlDbType.NVarChar);
                        command.Parameters.Add("@Address", SqlDbType.NVarChar);
                        command.Parameters.Add("@City", SqlDbType.NVarChar);
                        command.Parameters.Add("@Country", SqlDbType.NVarChar);
                        command.Parameters.Add("@PostalCode", SqlDbType.NVarChar);
                        command.Parameters.Add("@Phone", SqlDbType.NVarChar);

                        foreach (var item in clientes)
                        {
                            command.Parameters[0].Value = item.Id;
                            command.Parameters[1].Value = item.Name;
                            command.Parameters[2].Value = item.Address;
                            command.Parameters[3].Value = item.City;
                            command.Parameters[4].Value = item.Country;
                            command.Parameters[5].Value = item.PostalCode;
                            command.Parameters[6].Value = item.Phone;

                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine();








            
        }
    }
}
