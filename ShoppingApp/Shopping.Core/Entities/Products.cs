using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Core.Entities
{
    public class Products
    {
        public int ProductId { get; }
        public string ProductName { get; set; }

        public string ProductCategory { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal Discount { get; set; }

        
        public Products(int productId, string productName, string productCategory, string productDescription, decimal productPrice, decimal discount)
        {
            ProductId = productId;
            ProductName = productName;
            ProductCategory = productCategory;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            Discount = discount;
        }
    }
}
