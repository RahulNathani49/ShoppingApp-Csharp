using Microsoft.Data.SqlClient;
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
            connectionString= ConfigurationManager.ConnectionStrings["ShoppingCartDB"].ConnectionString;
        }
        public HashedPassword getPassword(string userName)
        {

            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select Salt,HashPassword from dbo.Users where UserName = @userName";
            cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value =userName;
            SqlDataReader reader = cmd.ExecuteReader();
            
            HashedPassword hp = GetPassword(reader);
            return hp;
        }

        public bool setLoggedIn(string userName,bool status)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            DateTime now = DateTime.UtcNow;
            cmd.CommandText = "update dbo.Users set IsLoggedIn = @value"
                + " where UserName= @userName;";
          
            cmd.Parameters.Add("@value", SqlDbType.Bit).Value = 1;

            cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value =userName;
            int i = cmd.ExecuteNonQuery();

            return i != 0;
        }

        private HashedPassword GetPassword(SqlDataReader reader)
        {
              byte[] salt = null;
              byte[] hash = null;
        
                if (reader.Read())
                {
                    salt= (byte[])reader[0];
                hash = (byte[])reader[1];
            }
      
            
            return new HashedPassword(salt, hash);
           
        }
    }


}
