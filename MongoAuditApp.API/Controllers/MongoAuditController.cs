using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoAuditApp.API.Exceptions;
using MongoAuditApp.API.Models;
using MongoAuditApp.API.Validations.ModelsValidations;
using MongoAuditApp.BLL.Dto;
using MongoAuditApp.BLL.Interfaces;

namespace MongoAuditApp.API.Controllers
{
    public class MongoAuditController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMongoAuditService _auditService;
        private readonly ILogger<MongoAuditController> _logger;

        public MongoAuditController(
           IMapper mapper,
           IMongoAuditService auditService,
           ILogger<MongoAuditController> logger)
        {
            _mapper = mapper;
            _auditService = auditService;
            _logger = logger;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _auditService.Get(id);
                if (result == null)
                {
                    return BadRequest("The ID entered is not associated with any apartment");
                }
                var mappedResult = _mapper.Map<MongoAuditModel>(result);

                return Ok(mappedResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApplicationException();
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] MongoAuditCreateModel model)
        {
            try
            {
                var auditValidator = new AuditValidator();
                auditValidator.CheckAuditPostModel(model);

                var mappedModel = _mapper.Map<MongoAuditDto>(model);
                var audit = await _auditService.Add(mappedModel);

                return Ok(audit);
            }
            catch (ModelValidationException e)
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
