using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.API.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }


        public OrderStatus Status { get; set; }

        public IList<OrderItem> Items { get; set; }


        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ModifiedDate { get; set; }

        public string ShippingAddressLine1 { get; set; }

        public string ShippingAddressLine2 { get; set; }

        public string ShippingAddressCity { get; set; }

        public string ShippingAddressState { get; set; }

        public string ShippingAddressZip { get; set; }

        
        public bool SplittingPrefference { get; set; }
    }

    [Flags]
    public enum OrderStatus
    {
        None = 0,
        Submitted = 1,
        Packed = 2,
        Paid = 4,
        Shipped = 8
    }
}
