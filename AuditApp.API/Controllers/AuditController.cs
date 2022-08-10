using AuditApp.API.Constants;
using AuditApp.API.Exceptions;
using AuditApp.API.Models;
using AuditApp.API.Validations.ModelsValidations;
using AuditApp.BLL.Dto;
using AuditApp.BLL.Exceptions;
using AuditApp.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditApp.API.Controllers
{
    [ApiController]
    [Route(RouteConstants.RouteAudit)]
    public class AuditController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly ILogger<AuditController> _logger;

        public AuditController(
           IMapper mapper,
           IAuditService auditService,
           ILogger<AuditController> logger)
        {
            _mapper = mapper;
            _auditService = auditService;
            _logger = logger;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _auditService.GetAll();
                var mappedResult = _mapper.Map<IList<AuditModel>>(result);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _auditService.Get(id);
                if (result == null)
                {
                    return BadRequest("The ID entered is not associated with any apartment");
                }
                var mappedResult = _mapper.Map<AuditModel>(result);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AuditCreateModel model)
        {
            try
            {
                var auditValidator = new AuditValidator();
                auditValidator.CheckAuditPostModel(model);

                var mappedModel = _mapper.Map<AuditDto>(model);
                var audit = await _auditService.Add(mappedModel);

                return Ok(audit);
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
        public async Task<ActionResult> Edit([FromBody] AuditModel model)
        {
            try
            {
                var auditValidator = new AuditValidator();
                auditValidator.CheckAuditPutModel(model);

                var mappedModel = _mapper.Map<AuditDto>(model);
                var isModified = await _auditService.Edit(mappedModel);

                if (isModified)
                {
                    return Ok("Audit row successfully modified!");
                }
                else
                {
                    return NotFound("Audit row not found or not modified!");
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
        public async Task<IActionResult> Delete([FromBody] AuditDeleteModel model)
        {
            try
            {
                var auditValidator = new AuditValidator();
                auditValidator.CheckAuditDeleteModel(model);

                var result = await _auditService.Delete(model.Id);
                if (result != null)
                {
                    return Ok($"The audit with id {result.Id} was deleted ");
                }
                else
                {
                    return NotFound("Audit row not found or not deleted!");
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
