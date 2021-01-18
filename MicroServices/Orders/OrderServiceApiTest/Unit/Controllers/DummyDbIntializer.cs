using System;
using System.Collections.Generic;
using System.Text;
using OrderService.API.Controllers;
using OrderService.API.Model;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace OrderServiceApiTest.Unit.Controllers
{
    public class DummyDbIntializer
    {
        protected DbContextOptions<OrderContext> ContextOptions { get; }

        protected DummyDbIntializer(DbContextOptions<OrderContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        private void Seed()
        {

            using (var context = new OrderContext(ContextOptions)) 
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new Order();
                //one.OrderId = 2;
                one.CustomerId = 3;
                one.Status = OrderStatus.Shipped;

                List<OrderItem> items1 = new List<OrderItem>();
                var item1 = new OrderItem();
                //item1.OrderItemId = 1;
                item1.OrderId = 2;
                item1.ProductId = 1;
                item1.ShippingMethodId = 2;
                item1.ProductName = "TestProduct 1";
                item1.Quantity = 1;
                item1.Tax = 2;
                item1.ShippingCharges = 3;
                item1.UnitPriceAmount = 12;
                item1.TotalLineAmount = 17;
                items1.Add(item1);

                var item2 = new OrderItem();
                //item2.OrderItemId = 2;
                item2.OrderId = 2;
                item2.ProductId = 2;
                item2.ShippingMethodId = 2;
                item2.ProductName = "TestProduct 2";
                item2.Quantity = 1;
                item2.Tax = 2;
                item2.ShippingCharges = 3;
                item2.UnitPriceAmount = 12;
                item2.TotalLineAmount = 17;
                items1.Add(item2);

                one.Items = items1;
                one.CreatedDate = new DateTime(2020, 12, 28);
                one.ModifiedDate = new DateTime(2020, 12, 28);
                one.ShippingAddressLine1 = "XYZ Street";
                one.ShippingAddressLine2 = "ABC";
                one.ShippingAddressCity = "Boston";
                one.ShippingAddressState = "Mass.";
                one.ShippingAddressZip = "12345";
                one.SplittingPrefference = true;


                var two = new Order();
                //two.OrderId = 3;
                two.CustomerId = 4;
                two.Status = OrderStatus.Paid;

                List<OrderItem> items2 = new List<OrderItem>();
                var item3 = new OrderItem();
                //item3.OrderItemId = 3;
                item3.OrderId = 3;
                item3.ProductId = 1;
                item3.ShippingMethodId = 2;
                item3.ProductName = "TestProduct 1";
                item3.Quantity = 1;
                item3.Tax = 2;
                item3.ShippingCharges = 3;
                item3.UnitPriceAmount = 12;
                item3.TotalLineAmount = 17;
                items2.Add(item3);

                var item4 = new OrderItem();
                //item4.OrderItemId = 4;
                item4.OrderId = 2;
                item4.ProductId = 2;
                item4.ShippingMethodId = 2;
                item4.ProductName = "TestProduct 2";
                item4.Quantity = 1;
                item4.Tax = 2;
                item4.ShippingCharges = 3;
                item4.UnitPriceAmount = 12;
                item4.TotalLineAmount = 17;
                items2.Add(item4);

                two.Items = items2;
                two.CreatedDate = new DateTime(2020, 12, 28);
                two.ModifiedDate = new DateTime(2020, 12, 28);
                two.ShippingAddressLine1 = "AbC Street";
                two.ShippingAddressLine2 = "XYZ";
                two.ShippingAddressCity = "Cambridge";
                two.ShippingAddressState = "Mass.";
                two.ShippingAddressZip = "12345";
                two.SplittingPrefference = false;

                context.AddRange(one, two);

                context.SaveChanges();
            }

        }
    }
}
