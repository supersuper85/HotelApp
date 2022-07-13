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
        private readonly ICustomerService _bookService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
           IMapper mapper,
           ICustomerService bookService,
           ILogger<CustomerController> logger)
        {
            _mapper = mapper;
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _bookService.GetAll();
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
