using AutoMapper;
using Customer.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Customer.API.Models;
using EventBus.Messages.Events;
using MassTransit;

namespace Customer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CustomerController(ICustomerRepository customerRepository, ILogger<CustomerController> logger, IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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
        [ProducesResponseType(typeof(Entities.Customer), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Entities.Customer>> CreateCustomerAsync([FromBody] CreateCustomerModel customerModel)
        {
            try
            {
                var customer = await _customerRepository.CreateCustomerAsync(_mapper.Map<Entities.Customer>(customerModel));
                return CreatedAtRoute("CustomerById", new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateCustomerAsync([FromBody] UpdateCustomerModel updateCustomerModel)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerAsync(updateCustomerModel.Id);
                if (customer == null)
                {
                    _logger.LogError($"Customer not found.");
                    return NotFound();
                }

                var updatedCustomer =
                    await _customerRepository.UpdateCustomerAsync(_mapper.Map(updateCustomerModel, customer));
                await _publishEndpoint.Publish(_mapper.Map<UpdateCustomerEvent>(customer));
                return Ok(updatedCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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