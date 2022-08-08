using System;
using Microsoft.AspNetCore.Mvc;
using HotelApp.API.Constants;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelApp.BLL.Dto;
using HotelApp.BLL.Interfaces;
using Microsoft.Extensions.Logging;
using HotelApp.API.Models.CustomerModels;
using HotelApp.API.Validations.ModelsValidations;
using HotelApp.API.Exceptions;
using HotelApp.BLL.Exceptions;
using HotelApp.API.Models.ApartmentModels;

namespace HotelApp.API.Controllers
{
    [ApiController]
    [Route(RouteConstants.RouteApartment)]
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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _apartmentService.GetAll();
                var mappedResult = _mapper.Map<IList<ApartmentModel>>(result);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }

        [HttpGet("getallavailableapartments")]
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

        [HttpGet("get")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _apartmentService.Get(id);
                if (result == null)
                {
                    return BadRequest("The ID entered is not associated with any apartment");
                }
                var mappedResult = _mapper.Map<ApartmentModel>(result);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ApartmentCreateModel model)
        {
            try
            {
                var apartmentValidator = new ApartmentValidator();
                apartmentValidator.CheckApartmentPostModel(model);

                var mappedModel = _mapper.Map<ApartmentDto>(model);
                var reservation = await _apartmentService.Add(mappedModel);

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

        [HttpPut("edit")]
        public async Task<ActionResult> Edit([FromBody] ApartmentEditModel model)
        {
            try
            {
                var apartmentValidator = new ApartmentValidator();
                apartmentValidator.CheckApartmentPutModel(model);

                var mappedModel = _mapper.Map<ApartmentDto>(model);
                var isModified = await _apartmentService.Edit(mappedModel);

                if (isModified)
                {
                    return Ok("Apartment successfully modified!");
                }
                else
                {
                    return NotFound("Apartment not found or not modified!");
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

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] ApartmentDeleteModel model)
        {
            try
            {
                var apartmentValidator = new ApartmentValidator();
                apartmentValidator.CheckApartmentDeleteModel(model);

                var result = await _apartmentService.Delete(model.Id);
                if (result != null)
                {
                    return Ok($"The apartment with id {result.Id} was deleted ");
                }
                else
                {
                    return NotFound("Apartment not found or not deleted!");
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
