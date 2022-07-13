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
    public class RawSqlCustomerAccountRepository : ICustomerAccountRepository
    {
        private readonly string _connectionString;

        public RawSqlCustomerAccountRepository(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"\"{nameof(connectionString)}\" не может быть неопределенным или пустым.", nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        public IReadOnlyList<CustomerAccount> GetAll()
        {
            var result = new List<CustomerAccount>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "SELECT [Id], [Login], [Password], [Balance] FROM [CustomerAccount]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new CustomerAccount(Convert.ToInt32(reader["Id"]),
                                                Convert.ToString(reader["Login"]),
                                                Convert.ToString(reader["Password"]),
                                                Convert.ToDecimal(reader["Balance"]))
               );
            }
            return result;

            

        }

        public CustomerAccount GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "SELECT [Id], [Login], [Password], [Balance] FROM [CustomerAccount] WHERE [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new CustomerAccount(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Login"]),
                    Convert.ToString(reader["Password"]),
                    Convert.ToDecimal(reader["Balance"])
                    );
            }
            else
            {
                return null;
            }
        }

        public CustomerAccount GetByLogin(string login)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "SELECT [Id], [Login], [Password], [Balance] FROM [CustomerAccount] WHERE [Login] = @login";
            sqlCommand.Parameters.Add("@login", SqlDbType.NVarChar, 20).Value = login;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new CustomerAccount(
                    Convert.ToInt32(reader["Id"]),
                    Convert.ToString(reader["Login"]),
                    Convert.ToString(reader["Password"]),
                    Convert.ToDecimal(reader["Balance"])
                    );
            }
            else
            {
                return null;
            }
        }

        public void Update(CustomerAccount customerAccount)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "UPDATE [CustomerAccount] SET [Login] = @login, [Password] = @password, [Balance] = @balance WHERE [Id] = @id";
            sqlCommand.Parameters.Add("@login", SqlDbType.NVarChar, 20).Value = customerAccount.Login;
            sqlCommand.Parameters.Add("@password", SqlDbType.NVarChar, 20).Value = customerAccount.Password;
            sqlCommand.Parameters.Add("@balance", SqlDbType.Int).Value = customerAccount.Balance;
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = customerAccount.Id;
            sqlCommand.ExecuteNonQuery();
            

        }

        public void Delete(CustomerAccount customerAccount)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "DELETE [CustomerAccount] WHERE [Id] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = customerAccount.Id;
            sqlCommand.ExecuteNonQuery();

            
        }

        public IReadOnlyList<Tuple<CustomerAccount, decimal>> GetAllTotalPrice()
        {
            var result = new List<Tuple<CustomerAccount, decimal>>();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "SELECT [c].Id, [Login], [Password], [Balance], SUM([Price]) AS [TotalPrice] FROM [CustomerAccount] c INNER JOIN [Order] [o] ON [o].CustomerId = [c].id " +
                                     " INNER JOIN [OrderProduct] [op] ON [op].OrderId = [o].Id INNER JOIN [Product] [p] ON [p].Id = [op].ProductId GROUP BY [Login], [c].Id, [Password], [Balance]";

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while(reader.Read())
            {
                CustomerAccount customer = new CustomerAccount(Convert.ToInt32(reader["Id"]), Convert.ToString(reader["Login"]),
                                                               Convert.ToString(reader["Password"]), Convert.ToInt32(reader["Balance"]));
                result.Add(new Tuple<CustomerAccount, decimal>(customer, Convert.ToDecimal(reader["TotalPrice"])));
            }
            return result;


        }
    }
}
