using AutoMapper;
using Azure.Core;
using DemoNet.Api.Data;
using DemoNet.Api.Exceptions;
using DemoNet.Api.Interfaces;
using DemoNet.Api.Models.Entities;
using DemoNet.Api.Models.VwModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DemoNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomerInfoesController : ControllerBase
    {
        private readonly ICustomerInfoRepository _repository;
        private readonly ILogger<CustomerInfoesController> _logger;
        private readonly IMapper _mapper;

        public CustomerInfoesController(ICustomerInfoRepository repository, ILogger<CustomerInfoesController> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        //[HttpGet, Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VmCustomerInfo>), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<VmCustomerInfo>>> GetCustomers()
        {
            var customers = await _repository.GetAllAsync();
            if (customers == null)
            {
                return NotFound("No data found");
            }
            //return Ok(new { data = _mapper.Map<List<VmCustomerInfo>>(customers) });
            return Ok(_mapper.Map<List<VmCustomerInfo>>(customers));
        }

        [HttpGet("{id:}", Name = "GetById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(VmCustomerInfo), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<VmCustomerInfo>> GetCustomerById(int id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
            {
                _logger.LogInformation($"Customer {id} Not Found");
                return NotFound($"Customer {id} Not Found");
            }

            return Ok(_mapper.Map<VmCustomerInfo>(customer));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerInfo), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<CustomerInfo>> CreateProduct([FromBody] CustomerInfo customer)
        {
            var newCustomer = await _repository.AddAsync(customer);

            _logger.LogInformation($"Customer {newCustomer.Id} is successfully created.");
            return CreatedAtRoute("GetById", new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPut(Name = "UpdateCustomer")]
        [ProducesResponseType(typeof(VmCustomerInfo), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateCustomer([FromBody] VmCustomerInfo customer)
        {
            var customerToUpdate = await _repository.GetByIdAsync(customer.Id);
            if (customerToUpdate == null)
            {
                throw new NotFoundException(nameof(CustomerInfo), customer.Id);
            }

            _mapper.Map(customer, customerToUpdate, typeof(VmCustomerInfo), typeof(CustomerInfo));

            await _repository.UpdateAsync(customerToUpdate);

            _logger.LogInformation($"Customer {customerToUpdate.Id} is successfully updated.");

            return Ok($"Customer {customerToUpdate.Id} is successfully updated.");
        }

        [HttpDelete("{id}", Name = "DeleteCustomer")]
        [ProducesResponseType(typeof(CustomerInfo), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var customerToDelete = await _repository.GetByIdAsync(id);
            if (customerToDelete == null)
            {
                throw new NotFoundException(nameof(CustomerInfo), id);
            }

            //_mapper.Map(customer, customerToDelete, typeof(VmCustomerInfo), typeof(CustomerInfo));

            await _repository.DeleteAsync(customerToDelete);

            _logger.LogInformation($"Customer {customerToDelete.Id} is successfully deleted.");

            return Ok($"Customer {customerToDelete.Id} is successfully deleted.");
        }

    }
}
