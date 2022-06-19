using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Core.Entities
{
    public class Cart
    {
        public Cart(string productName, string productDescription ,decimal productPrice, int productQuantity, decimal discount)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            ProductDescription = productDescription;
            ProductQuantity = productQuantity;
            Discount = discount;
        }
        public Cart( int productId, int productQuantity)
        {
            ProductId = productId;
            ProductQuantity= productQuantity;

        }
        public int ProductId
        {
            get; set;
        }
        public string ProductName
        {
            get; set;
        }

        public decimal ProductPrice
        {
            get; set;
        }
        public string ProductDescription
        {
            get; set;
        }

        public int ProductQuantity
        {
            get; set;
        }

        public Decimal ItemTotal
        {
            get; set;
        }

        public decimal Discount
        {
            get;set;
        }


    }
}
