using AutoMapper;
using HotelApp.API.InputModels;
using HotelApp.API.Models;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API.Controllers
{
    [ApiController]
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

        [HttpGet("GetAllReservationsWithTheirCustomers")]
        public async Task<IActionResult> GetAllReservationsWithTheirCustomers()
        {
            try
            {
                var result = await _reservationService.GetAllReservationsWithTheirCustomers();
                var mappedResult = _mapper.Map<IList<ReservationModel>>(result);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        [HttpPost("CreateReservation")]
        public async Task<IActionResult> Create([FromBody] ReservationInputModel model)
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
