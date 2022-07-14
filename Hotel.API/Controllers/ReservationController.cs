using AutoMapper;
using HotelApp.API.Models;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IReservationService _reservationService;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(
           IMapper mapper,
           IReservationService reservationService,
           ILogger<ReservationController> logger)
        {
            _mapper = mapper;
            _reservationService = reservationService;
            _logger = logger;
        }

        [HttpPost("CreateReservation")]
        public async Task<IActionResult> Create([FromBody] ReservationModel model)
        {
            try
            {
                var mappedModel = _mapper.Map<ReservationDto>(model);
                var reservation = await _reservationService.Add(mappedModel);

                return Ok(reservation);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
    }
}
