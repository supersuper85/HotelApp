using System;
using Microsoft.AspNetCore.Mvc;
using HotelApp.API.Constants;
using AutoMapper;
using HotelApp.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using Microsoft.Extensions.Logging;
using HotelApp.API.Models.CustomerModels;
using HotelApp.API.Exceptions;
using HotelApp.BLL.Exceptions;
using HotelApp.API.Validations.ModelsValidations;

namespace HotelApp.API.Controllers
{
    [ApiController]
    [Route(RouteConstants.RouteCustomer)]
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
           IMapper mapper,
           ICustomerService customerService,
           ILogger<CustomerController> logger)
        {
            _mapper = mapper;
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _customerService.GetAll();
                var mappedResult = _mapper.Map<IList<CustomerModel>>(result);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetAReservationWithHisCustomer(int id)
        {
            try
            {
                var result = await _customerService.Get(id);
                if (result == null)
                {
                    return BadRequest("The ID entered is not associated with any customer");
                }
                var mappedResult = _mapper.Map<CustomerModel>(result);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CustomerCreateModel model)
        {
            try
            {
                var customerValidator = new CustomerValidator();
                customerValidator.CheckCustomerPostModel(model);

                var mappedModel = _mapper.Map<CustomerDto>(model);
                var reservation = await _customerService.Add(mappedModel);

                return Ok(reservation);
            }
            catch (ModelValidationException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (DatabaseValidatorException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromBody] CustomerEditModel model)
        {
            try
            {
                var customerValidator = new CustomerValidator();
                customerValidator.CheckCustomerPutModel(model);

                var mappedModel = _mapper.Map<CustomerDto>(model);
                var isModified = await _customerService.Edit(mappedModel);

                if (isModified)
                {
                    return Ok("Customer successfully modified!");
                }
                else
                {
                    return NotFound("Customer not found or not modified!");
                }

            }
            catch (ModelValidationException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (DatabaseValidatorException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] CustomerDeleteModel model)
        {
            try
            {
                var customerValidator = new CustomerValidator();
                customerValidator.CheckReservationDeleteModel(model);

                var result = await _customerService.Delete(model.Id);
                if (result != null)
                {
                    return Ok($"Customer with id {result.Id} was deleted ");
                }
                else
                {
                    return NotFound("Customer not found or not deleted!");
                }
            }
            catch (ModelValidationException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (DatabaseValidatorException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }
    }
}
