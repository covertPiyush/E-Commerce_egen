using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BulkOrderService.API.Model;
using EFCore.BulkExtensions;
using Microsoft.Extensions.Logging;

namespace BulkOrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkOrdersController : ControllerBase
    {
        private readonly BulkOrderContext _context;
        private readonly ILogger<BulkOrdersController> logger;

        public BulkOrdersController(BulkOrderContext context, ILogger<BulkOrdersController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        

        // PUT: api/BulkOrders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutOrder([FromBody]List<Order> orders)
        {
            try
            {
                logger.LogInformation("Updating Bulk Orders");
                await _context.BulkInsertOrUpdateAsync(orders);
                return StatusCode(StatusCodes.Status200OK, orders);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex); ;
            }
            

            
        }

        // POST: api/BulkOrders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder([FromBody] List<Order> orders)
        {
            

            try
            {
                logger.LogInformation("Creating Bulk Orders");
                await _context.BulkInsertAsync(orders);
               return StatusCode(StatusCodes.Status201Created, orders);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex); ;
            }

        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }

        
    }
}
