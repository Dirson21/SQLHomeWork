using SQLHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLHomeWork.Repositories
{
    public class RawSqlProductRepository : IProductRepository
    {
        readonly string _connectionString;

        public RawSqlProductRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"\"{nameof(connectionString)}\" не может быть неопределенным или пустым.", nameof(connectionString));
            }

            _connectionString = connectionString;

        }
        public Product GetByName(string name)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "SELECT [Id], [Name], [Price] FROM [Product] WHERE [Name] = @name";
            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar, 20).Value = name;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new Product(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Name"]), 
                    Convert.ToDecimal(reader["Price"])
                    );
            }
            else
            {
                return null;
            }

        }

       
    }
}
