using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.API.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string Price { get; set; }

        public string ProductDescription { get; set; }

    }
}
