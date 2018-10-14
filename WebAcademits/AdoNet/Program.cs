using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StudyServer"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.WriteState();
                connection.WriteProductsCount();
                connection.AddCategory("Other");
                connection.AddProduct("other product", 5);
                connection.EditProduct(38, "other prod", 4);
                connection.DeleteProduct(38);

                connection.WriteAllProducts2();
            }

            Console.Read();
        }

        private static void WriteState(this SqlConnection connection)
        {
            Console.WriteLine("Connection state = " + connection.State);
        }


        private static void WriteProductsCount(this SqlConnection connection)
        {
            var query = @"SELECT COUNT(*) FROM dbo.Product";
            using (var command = new SqlCommand(query, connection))
            {
                Console.WriteLine("Products count: " + command.ExecuteScalar());
            }
        }

        private static void AddCategory(this SqlConnection connection, string name)
        {
            var param = new SqlParameter("@newCategory", name);
            var query = @"INSERT INTO dbo.Category(Name) VALUES(@newCategory)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add(param);
                command.ExecuteNonQuery();
                Console.WriteLine($"Category <{name}> added to database");
            }
        }

        private static void AddProduct(this SqlConnection connection, string name, int categoryId)
        {
            var nameParam = new SqlParameter("@newProduct", name);
            var query = $"INSERT INTO dbo.Product(Name, CategoryId) VALUES(@newProduct, {categoryId})";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add(nameParam);
                command.ExecuteNonQuery();
                Console.WriteLine($"Product <{name}> added to database, categoryID: {categoryId}");
            }
        }

        private static void EditProduct(this SqlConnection connection, int id, string name = null, int? categoryId = null)
        {
            if (name == null && categoryId == null)
            {
                return;
            }

            if (name != null)
            {
                var param = new SqlParameter("@newName", name);
                var query = $"UPDATE dbo.Product SET Name = @newName WHERE Id = {id}";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(param);
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Product with id = {id} updated: name = {name}");
                }
            }

            if (categoryId != null)
            {
                var query = $"UPDATE dbo.Product SET CategoryId = {categoryId.Value} WHERE Id = {id}";
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Product with id = {id} updated: category = {categoryId.Value}");
                }
            }
        }

        private static void DeleteProduct(this SqlConnection connection, int id)
        {
            var query = $"DELETE FROM dbo.Product WHERE Id = {id}";
            using (var command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine($"Product with id = {id} deleted");
            }
        }

        private static SqlCommand GetAllCommand(this SqlConnection connection)
        {
            var query =
                @"SELECT * FROM dbo.Product INNER JOIN dbo.Category ON dbo.Product.CategoryId = dbo.Category.Id;";
            return new SqlCommand(query, connection);
        }

        private static void WriteAllProducts(this SqlConnection connection)
        {
            Console.WriteLine();
            Console.WriteLine();
            using (var command = connection.GetAllCommand())
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}     {1}", reader[1], reader[4]);
                    }
                }
            }
        }

        private static void WriteAllProducts2(this SqlConnection connection)
        {
            Console.WriteLine();
            Console.WriteLine();
            using (var command = connection.GetAllCommand())
            {
                using (var adapter = new SqlDataAdapter(command))
                {
                    var dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    var table = dataSet.Tables[0];

                    foreach (DataRow row in table.Rows)
                    {
                        Console.WriteLine(row[1] + "   " + row[4]);
                    }

                }
            }
        }

    }
}
