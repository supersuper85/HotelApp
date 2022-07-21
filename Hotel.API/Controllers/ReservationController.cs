﻿using AutoMapper;
using HotelApp.API.Exceptions;
using HotelApp.API.Models.OtherInputModels;
using HotelApp.API.Models.ReservationModels;
using HotelApp.API.Validations;
using HotelApp.API.Validations.ModelsValidations;
using HotelApp.API.Validations.OtherInputModelsValidations;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Exceptions;
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

        [HttpGet("GetAReservationWithHisCustomer")]
        public async Task<IActionResult> GetAReservationWithHisCustomer(int id)
        {
            try
            {
                var result = await _reservationService.GetAReservationWithHisCustomer(id);
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
                return BadRequest();
            }
        }

        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationPostModel model)
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
                return BadRequest();
            }
        }

        [HttpPut("EditReservation")]
        public async Task<ActionResult> EditReservation([FromBody] ReservationPutModel model)
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

        [HttpDelete("DeleteReservation")]
        public async Task<IActionResult> DeleteReservation([FromBody] IdModel model)
        {
            try
            {
                var reservationValidator = new IdModelValidator();
                reservationValidator.CheckIdModel(model);

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
                return BadRequest();
            }
        }
    }
}
