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
        private readonly IAsyncRepository<Entities.Customer> _customerRepository;
        private readonly ILogger<CustomerController> _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CustomerController(IAsyncRepository<Entities.Customer> customerRepository, ILogger<CustomerController> logger, IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<Entities.Customer>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<Entities.Customer>>> CustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet]
        [Route("{id:int}", Name = "CustomerById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Entities.Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Customer>> CustomerByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var customer = await _customerRepository.GetByIdAsync(id);

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
                var customer = await _customerRepository.AddAsync(_mapper.Map<Entities.Customer>(customerModel));
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateCustomerAsync([FromBody] UpdateCustomerModel updateCustomerModel)
        {
            try
            {
                API.Entities.Customer customer = await _customerRepository.GetByIdAsync(updateCustomerModel.Id);
                if (customer == null)
                {
                    _logger.LogError($"Customer not found.");
                    return NotFound();
                }

                if (customer.FirstName != updateCustomerModel.FirstName ||
                    customer.LastName != updateCustomerModel.LastName)
                {
                    await _customerRepository.UpdateAsync(_mapper.Map(updateCustomerModel, customer));
                    var publishedEvent = _mapper.Map<UpdateCustomerEvent>(customer);
                    await _publishEndpoint.Publish(publishedEvent);
                }
                else
                {
                    await _customerRepository.UpdateAsync(_mapper.Map(updateCustomerModel, customer));
                }
                
                return Ok();
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
            var customer = await _customerRepository.GetByIdAsync(id);
            await _customerRepository.DeleteAsync(customer);
            return NoContent();
        }

    }
}