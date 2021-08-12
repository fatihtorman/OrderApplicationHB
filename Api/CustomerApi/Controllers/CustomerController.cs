using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerRepository customerRepository, ILogger<CustomerController> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        // GET: api/Customer
        [HttpGet]
        public IEnumerable<Customer> GetCustomer()
        {
            return _customerRepository.GetAll();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customers = _customerRepository.GetById(id);

            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            try
            {
                _customerRepository.Update(customer);
                _customerRepository.Commit();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
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

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _customerRepository.Add(customers);
                _customerRepository.Commit();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var customer = _customerRepository.GetById(customers.Id);
            return Ok(customer.Id);
            //return CreatedAtAction("GetCustomers", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customers = _customerRepository.GetById(id);
            if (customers == null)
            {
                return NotFound();
            }


            try
            {
                _customerRepository.Remove(customers);
                _customerRepository.Commit();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(customers);
        }

        private bool CustomersExists(int id)
        {
            return _customerRepository.Find(e => e.Id == id).Any();
        }

        // GET: api/Customer/validate/5
        [HttpGet("validate/{id}")]
        public IActionResult ValidateCustomer([FromRoute] int id)
        {
            if (_customerRepository.Find(e => e.Id == id).Any())
                return Ok();
            else
            {
                return NotFound();
            }


        }
    }
}
