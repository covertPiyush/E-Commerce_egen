using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderService.API.Model;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderContext _context;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(OrderContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        


        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            logger.LogInformation("Getting all Orders");
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            logger.LogInformation($"Getting Order with ID -{ id }");
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                logger.LogWarning($"Order with ID -{ id } not found");
                return NotFound();

            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            logger.LogInformation($"Updating Order with ID -{ id }");
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException Dbex)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogWarning($"Order with ID -{ id } does not exist");
                    return StatusCode(StatusCodes.Status500InternalServerError, Dbex); 
                }
            }

             return StatusCode(StatusCodes.Status204NoContent);
        }

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                logger.LogInformation("Creating Order");
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, order);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex); 
            }

            ;
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            logger.LogInformation($"Deleting Order with ID -{ id }");
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return NotFound();
                }
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status202Accepted, order);

            }
            catch(Exception ex) {
                logger.LogWarning(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
