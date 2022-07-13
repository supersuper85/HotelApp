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
        private readonly IApartmentService _hotelRoomService;
        private readonly ILogger<ApartmentController> _logger;

        public ApartmentController(
           IMapper mapper,
           IApartmentService hotelRoomService,
           ILogger<ApartmentController> logger)
        {
            _mapper = mapper;
            _hotelRoomService = hotelRoomService;
            _logger = logger;
        }

        [HttpGet("GetAllAvailableRooms")]
        public async Task<IActionResult> GetAllAvailableRooms()
        {
            try
            {
                var result = await _hotelRoomService.GetAllAvailableHotelRooms();
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
