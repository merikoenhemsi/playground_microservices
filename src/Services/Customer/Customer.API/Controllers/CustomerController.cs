using Customer.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerRepository customerRepository, ILogger<CustomerController> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("items")]
        [ProducesResponseType(typeof(IReadOnlyList<Entities.Customer>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<Entities.Customer>>> CustomersAsync()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            return Ok(customers);
        }

        [HttpGet]
        [Route("items/{id:int}", Name = "CustomerById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Entities.Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Customer>> CustomerByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var customer = await _customerRepository.GetCustomerAsync(id);

            if (customer == null)
            {
                _logger.LogError($"Customer not found.");
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Entities.Customer), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Entities.Customer>> CreateCustomerAsync([FromBody] Entities.Customer customer)
        {
            await _customerRepository.CreateCustomerAsync(customer);

            return CreatedAtRoute("CustomerById", new { id = customer.Id }, customer);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Entities.Customer), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] Entities.Customer customer)
        {
            return Ok(await _customerRepository.UpdateCustomerAsync(customer));
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteCustomerByIdAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
            return NoContent();
        }

    }
}