using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Core.Entities
{
    public class Order
    {
        public int OrderId { get;  }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
        public int Total { get; set; }

    }
}
