using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkOrderService.API.Model
{
    public class OrderItem
    {

        public int OrderItemId { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ShippingMethodId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public double UnitPriceAmount { get; set; }

        public double Tax { get; set; }

        public double ShippingCharges { get; set; }

        public double TotalLineAmount { get; set; }
    }
}
