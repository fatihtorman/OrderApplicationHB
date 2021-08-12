using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private IOrderRepository _orderRepository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController( IOrderRepository orderRepository, ILogger<OrdersController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        // GET: api/Order
        [HttpGet]
        public IEnumerable<Order> GetOrder()
        {
            return _orderRepository.GetAll();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public IActionResult GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            try
            {
                 _orderRepository.Update(order);
                 _orderRepository.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Orders/5
        [HttpPut("change/{id}")]
        public async Task<IActionResult> ChangeStatusOrder([FromRoute] int id, [FromBody] string status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != orders.Id)
            //{
            //    return BadRequest();
            //}

            var order =  _orderRepository.GetById(id);
            order.Status = status;

            try
            {
                 _orderRepository.Update(order);
                _orderRepository.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _orderRepository.Add(orders);
                _orderRepository.Commit();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var order = _orderRepository.GetById(orders.Id);
            return Ok(order.Id);
            //return CreatedAtAction("GetOrder", new { id = orders.Id }, orders);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order =  _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            try
            {
                 _orderRepository.Remove(order);
                _orderRepository.Commit();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(order);
        }

        private bool OrderExists(int id)
        {
            return _orderRepository.Find(e => e.Id == id).Any();
        }
    }
}
