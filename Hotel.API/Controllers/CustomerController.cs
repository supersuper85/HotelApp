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
    }
}
