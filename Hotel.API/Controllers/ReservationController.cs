using AutoMapper;
using HotelApp.API.Constants;
using HotelApp.API.Exceptions;
using HotelApp.API.Models.ReservationModels;
using HotelApp.API.Validations.ModelsValidations;
using HotelApp.API.Validations;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Exceptions;
using HotelApp.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API.Controllers
{
    [ApiController]
    [Route(RouteConstants.RouteReservation)]
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

        [HttpGet("GetAll")]
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
                throw new ApplicationException();
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetAReservationWithHisCustomer(int id)
        {
            try
            {
                var result = await _reservationService.GetAReservationById(id);
                if(result == null)
                {
                    return BadRequest("The ID entered is not associated with any reservation");
                }
                var mappedResult = _mapper.Map<ReservationModel>(result);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationCreateModel model)
        {
            try
            {
                var reservationValidator = new ReservationValidator();
                reservationValidator.CheckReservationPostModel(model);

                var mappedModel = _mapper.Map<ReservationDto>(model);
                var reservation = await _reservationService.Add(mappedModel);

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
        public async Task<ActionResult> EditReservation([FromBody] ReservationEditModel model)
        {
            try
            {
                var reservationValidator = new ReservationValidator();
                reservationValidator.CheckReservationPutModel(model);

                var mappedModel = _mapper.Map<ReservationDto>(model);
                var isModified = await _reservationService.EditAReservation(mappedModel);

                if (isModified)
                {
                    return Ok("Reservation successfully modified!");
                }
                else
                {
                    return NotFound("Reservation not found or not modified!");
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
        public async Task<IActionResult> DeleteReservation([FromBody] ReservationDeleteModel model)
        {
            try
            {
                var reservationValidator = new ReservationValidator();
                reservationValidator.CheckReservationDeleteModel(model);

                var result = await _reservationService.Delete(model.Id);
                if (result != null)
                {
                    return Ok($"Reservation with id {result.Id} was deleted ");
                }
                else
                {
                    return NotFound("Reservation not found or not deleted!");
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
