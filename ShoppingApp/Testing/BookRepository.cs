
using Microsoft.Data.SqlClient;
using Shopping.Presentation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Repositories
{
    
    public class BookRepository
    {
        private readonly string connectionString;
        
        public BookRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingCartDB"].ConnectionString;
        }
    

        public void CreateBook(Users user)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Users(UserName,HashPassword,Salt,IsLoggedIn,CreatedOn)  " +
                              "OUTPUT Inserted.UserId " +
                              "VALUES(@UserName, @HashPassword, @Salt,@IsLoggedIn, @CreatedOn);";

            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
            cmd.Parameters.Add("@HashPassword", SqlDbType.Binary).Value = user.HashPassword;
            cmd.Parameters.Add("@Salt", SqlDbType.Binary).Value =user.Salt ;
            cmd.Parameters.Add("@IsLoggedIn", SqlDbType.Bit).Value = user.IsLoggedIn;
            cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime2).Value = DateTime.Now;
            
            cmd.ExecuteScalar();
           
        }
   

    }
}
