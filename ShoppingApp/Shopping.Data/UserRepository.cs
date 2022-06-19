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

        public List<Cart> getCartDetails(int userID)
        {
            List<Cart> cartDetails = new List<Cart>();
            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select Inventory.ProductName,Inventory.ProductDescription,Inventory.ProductPrice, " +
                "Cart.Quantity,Inventory.Discount from Inventory join Cart  on " +      
                "Inventory.ProductID = Cart.ProductID " +
                " where Cart.UserId = @userID; ";
            cmd.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
            SqlDataReader reader = cmd.ExecuteReader();

            
            while (reader.Read())
            {
                cartDetails.Add(getCartList(reader));

            }

            return cartDetails;

        }

        public bool emptyCart(int userId)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "delete from dbo.cart where UserId= @userId";
                

            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
           

            int i = cmd.ExecuteNonQuery();
            return i != 0;
        }

        public List<Order> displayOrderHistory(int userId)
        {
            List<Order> orders = new List<Order>();

            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select orders.OrderId,Orders.quantity,ProductName,ProductPrice " +
                                "from Orders inner join Inventory " +
                                "on Inventory.ProductID = Orders.ProductId " +
                                    "where Orders.OrderId in(select OrderId from UserToOrderMap where UserId = @userId);";
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                orders.Add(createOrderObj(reader));
            }

            return orders;
        }

        private Order createOrderObj(SqlDataReader reader)
        {
            return new Order(
                            reader.GetInt32(0),
                            reader.GetString(2),
                            reader.GetInt32(1),
                            reader.GetDecimal(3)
                         );
        }

        public List<int> getOrderIds(int userID)
        {
            List<int> orderidlist = new List<int>();

            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select OrderId from dbo.UserToOrderMap where UserId  = @userID";
            cmd.Parameters.Add("@userID", SqlDbType.NVarChar).Value = userID;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                orderidlist.Add(reader.GetInt32(0));
            }

            return orderidlist;
        }

        public bool createOrder(Cart itemInUserCart, int orderId)
        { 
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO dbo.Orders(OrderId,ProductId,Quantity) " +
                              "VALUES(@orderId,@productId,@quantity);";

            cmd.Parameters.Add("@orderId", SqlDbType.Int).Value = orderId;
            cmd.Parameters.Add("@productId", SqlDbType.Int).Value = itemInUserCart.ProductId;
            cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = itemInUserCart.ProductQuantity;


            int id=cmd.ExecuteNonQuery();
            if (id > 0)
            {
                return true;
            }
            return false;
            
        }

        public List<Cart> getItems(int userId)
        {
            List<Cart> items = new List<Cart>();
            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select ProductId,Quantity from dbo.Cart where UserId=@userId";
            cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(GetItemsFromReader(reader));
            }
            return items;
        }
        private Cart GetItemsFromReader(SqlDataReader reader)
        {
            return new Cart(
                           reader.GetInt32(0),
                           reader.GetInt32(1)
                        );
        }

        public int createOrderId(int userID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO dbo.UserToOrderMap(UserId) " +
                              "OUTPUT Inserted.OrderId " +
                              "VALUES(@UserId);";

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userID;
        

            int id = (int)cmd.ExecuteScalar();
            if (id > 0)
            {
                return id;
            }
            return 0;
        }

        private Cart getCartList(SqlDataReader reader)
        {
            return new Cart(
                            reader.GetString(0),
                            reader.GetString(1),
                            reader.GetDecimal(2),
                            reader.GetInt32(3),
                            reader.GetDecimal(4)
                        );
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
        public string getUserName(string userName)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select UserName from dbo.Users1 where UserName = @userName";
            cmd.Parameters.Add("@userName", SqlDbType.NVarChar).Value = userName;
            SqlDataReader reader = cmd.ExecuteReader();
            string name;
            if (reader.Read())
            {
                name = reader.GetString(0);
                return name;
            }
            return null;
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