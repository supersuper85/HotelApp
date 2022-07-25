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
    }
}
