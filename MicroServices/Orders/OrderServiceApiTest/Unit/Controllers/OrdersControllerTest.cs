using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OrderService.API.Controllers;
using OrderService.API.Model;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace OrderServiceApiTest.Unit.Controllers
{
    public class OrdersControllerTest : DummyDbIntializer
    {
        public static string connectionString = "Data Source = DESKTOP-GV0HBA1\\SQLExpress2019; Initial Catalog = UnitTestDB; persist security info = True ; user id = sa; password= 123456789";
        //public Mock<ILogger<OrdersController>> loggerMock = new Mock<ILogger<OrdersController>>();
        public ILogger<OrdersController> logger = new Logger<OrdersController>(new NullLoggerFactory());
        public OrdersControllerTest()
        : base(
            new DbContextOptionsBuilder<OrderContext>()
                .UseSqlServer(connectionString)
                .Options)
        {
        }


        [Fact]
        public async void orders_Controller_get_should_return_all_Orders()
        {
            using (var context = new OrderContext(ContextOptions))
            {
                var controller = new OrdersController(context, logger);

                //Act
                var data = await controller.GetOrders();

                //Assert
                var items = Assert.IsType<List<Order>>(data.Value);
                Assert.Equal(2, items.Count);
            }

        }

        [Fact]
        public async void orders_Controller_get_should_return_particular_Order()
        {
            using (var context = new OrderContext(ContextOptions))
            {
                var controller = new OrdersController(context, logger);

                //Act
                var data = await controller.GetOrder(1);

                //Assert
                Assert.IsType<List<Order>>(data.Value);
                Assert.IsType<OkObjectResult>(data.Result);
            }

        }

        [Fact]
        public async void orders_Controller_get_by_id_should_return_not_found()
        {
            using (var context = new OrderContext(ContextOptions))
            {
                var controller = new OrdersController(context, logger);

                //Act
                var data = await controller.GetOrder(3000);

                // Assert
                Assert.IsType<NotFoundResult>(data.Result);
            }

        }


        public async void orders_Controller_add_order_should_return_not_found()
        {
           
            using (var context = new OrderContext(ContextOptions))
            {
                var one = new Order();
                //one.OrderId = 2;
                one.CustomerId = 3;
                one.Status = OrderStatus.Shipped;

                List<OrderItem> items1 = new List<OrderItem>();
                var item1 = new OrderItem();
                //item1.OrderItemId = 1;
                item1.OrderId = one.OrderId;
                item1.ProductId = 1;
                item1.ShippingMethodId = 2;
                item1.ProductName = "TestProduct B";
                item1.Quantity = 1;
                item1.Tax = 2;
                item1.ShippingCharges = 3;
                item1.UnitPriceAmount = 12;
                item1.TotalLineAmount = 17;
                items1.Add(item1);

                var item2 = new OrderItem();
                //item2.OrderItemId = 2;
                item2.OrderId = one.OrderId;
                item2.ProductId = 2;
                item2.ShippingMethodId = 2;
                item2.ProductName = "TestProduct A";
                item2.Quantity = 1;
                item2.Tax = 2;
                item2.ShippingCharges = 3;
                item2.UnitPriceAmount = 12;
                item2.TotalLineAmount = 17;
                items1.Add(item2);

                one.Items = items1;
                one.CreatedDate = new DateTime(2020, 12, 28);
                one.ModifiedDate = new DateTime(2020, 12, 28);
                one.ShippingAddressLine1 = "cbd Street";
                one.ShippingAddressLine2 = "ndx";
                one.ShippingAddressCity = "New York";
                one.ShippingAddressState = "New York.";
                one.ShippingAddressZip = "12345";
                one.SplittingPrefference = true;
                var controller = new OrdersController(context, logger);

                //Act
                var data  = await controller.PostOrder(one) ;

                // Assert
                Assert.IsType<List<Order>>(data.Value);
            }

        }


    }
}




    

