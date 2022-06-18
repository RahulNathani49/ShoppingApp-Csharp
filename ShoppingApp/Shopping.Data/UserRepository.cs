using Microsoft.Data.SqlClient;
using Shopping.Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Authentication;

namespace Shopping.Data
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingCartDB"].ConnectionString;
        }
        public HashedPassword getPassword(string userName)
        {

            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select HashPassword,Salt from dbo.Users1 where UserName = @userName";
            cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = userName;
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                HashedPassword hp = GetPassword(reader);
                return hp;
            }

            return null;
        }
        public List<Products> GetProducts()
        {
            List<Products> products = new List<Products>();
            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from dbo.Inventory";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(GetProductFromReader(reader));
            }
            return products;
        }

        public bool updateQuantity(int userID, int productId, int quantity)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "update dbo.cart set Quantity += @value"
                + " where UserID= @userId AND ProductID = @productId";

            cmd.Parameters.Add("@value", SqlDbType.Int).Value = quantity;
            cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

            cmd.Parameters.Add("@productId", SqlDbType.Int).Value = productId;

            int i = cmd.ExecuteNonQuery();
            return i != 0;
        }

        public bool checkIfItemExists(int userID, int productId)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select count(CartID) from dbo.Cart where UserID = @userID AND ProductID = @productId";
            cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
            cmd.Parameters.Add("@productId", SqlDbType.Int).Value = productId;
            int count = (int)cmd.ExecuteScalar();
          
            return count > 0;
        }

        public bool addToCart(int userID, int productId, int quantity)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO dbo.Cart(UserId,ProductID, Quantity) " +
                              "OUTPUT Inserted.CartId " +
                              "VALUES(@UserId, @ProductID, @Quantity);";

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userID;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
          
            int id = (int)cmd.ExecuteScalar();
            if (id > 0)
            {
                return true;
            }
            return false;
        }


        public int getUserId(string userName)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select UserId from dbo.Users1 where UserName = @userName";
            cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = userName;
            SqlDataReader reader = cmd.ExecuteReader();

            int userId;
            if (reader.Read())
            {
                userId=reader.GetInt32(0);
                return userId;
            }
            return 0;

        }

        private Products GetProductFromReader(SqlDataReader reader)
        {
            return new Products(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetDecimal(4),
                            reader.GetDecimal(5)
                        );
        }
        public bool setLoggedIn(string userName, bool status)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            DateTime now = DateTime.UtcNow;
            cmd.CommandText = "update dbo.Users1 set IsLoggedIn = @value"
                + " where UserName= @userName;";

            cmd.Parameters.Add("@value", SqlDbType.Bit).Value = 1;

            cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = userName;
            int i = cmd.ExecuteNonQuery();

            return i != 0;
        }

        private HashedPassword GetPassword(SqlDataReader reader)
        {
            byte[] salt = (byte[])reader[1];
            byte[] hash = (byte[])reader[0];
            return new HashedPassword(salt, hash);

        }
    }


}