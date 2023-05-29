using DemoNet.Api.Interfaces;
using DemoNet.Api.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DemoNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerInfoesController : ControllerBase
    {
        private readonly ICustomerInfoRepository _repository;
        private readonly ILogger<CustomerInfoesController> _logger;

        public CustomerInfoesController(ICustomerInfoRepository repository, ILogger<CustomerInfoesController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/CustomerInfoes
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerInfo>), (int)HttpStatusCode.OK)]
        //[ResponseCache(Duration =20)]
        public async Task<ActionResult<IEnumerable<CustomerInfo>>> GetCustomers()
        {
            var customers = await _repository.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id:}", Name = "GetCustomer")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomerInfo), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerInfo>> GetCustomerById(int id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerInfo), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerInfo>> CreateProduct([FromBody] CustomerInfo customer)
        {
            await _repository.AddAsync(customer);

            return CreatedAtRoute("GetCustomerById", new { id = customer.Id }, customer);
        }

    }
}
