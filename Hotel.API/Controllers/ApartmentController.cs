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
    public class ApartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IApartmentService _apartmentService;
        private readonly ILogger<ApartmentController> _logger;

        public ApartmentController(
           IMapper mapper,
           IApartmentService apartmentService,
           ILogger<ApartmentController> logger)
        {
            _mapper = mapper;
            _apartmentService = apartmentService;
            _logger = logger;
        }

        [HttpGet("GetAllAvailableApartments")]
        public async Task<IActionResult> GetAllAvailableApartments()
        {
            try
            {
                var result = await _apartmentService.GetAllAvailableHotelRooms();
                var mappedResult = _mapper.Map<IList<ApartmentModel>>(result);

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
