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

namespace HotelApp.API.Controllers
{
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

        [HttpGet("getAll")]
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
                return BadRequest();
            }
        }
    }
}
