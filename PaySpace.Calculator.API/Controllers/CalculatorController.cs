using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.API.Security;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize(DummyTokenAuthorizationPolicy.PolicyName)]
    public  class CalculatorController: ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IHistoryService _historyService;
        private readonly ITaxCalculationService _taxCalculationService;
        private readonly IMapper _mapper;
        private readonly IPostalCodeService _postalCodeService;
        public CalculatorController(
               ILogger<CalculatorController> logger,
               IHistoryService historyService,
               IPostalCodeService postalCodeService,
               ITaxCalculationService taxCalculationService,
               IMapper mapper)
        { 
            _historyService = historyService;
            _mapper = mapper;
            _logger = logger;
            _taxCalculationService = taxCalculationService;
            _postalCodeService = postalCodeService;

        }

        [HttpPost("calculateTax")]
        public async Task<ActionResult<CalculateResult>> Calculate(CalculateRequest request)
        {
            try
            {
                var taxCalculation = await _taxCalculationService.CalculateTaxAsync(request.Income, request.PostalCode);

                // Add calculation history
                await _historyService.AddAsync(new CalculatorHistory
                {
                    Tax = taxCalculation.Tax,
                    Calculator = taxCalculation.Calculator,
                    PostalCode = request.PostalCode,
                    Income = request.Income
                });

                // Map the result to a DTO and return
                var resultDto = _mapper.Map<CalculateResultDto>(taxCalculation);
                return Ok(resultDto);
              
            }
            catch (CalculatorException e)
            {
                // Log the exception and return a BadRequest response
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("history")]
        public async Task<ActionResult<List<CalculatorHistory>>> History()
        {
            var history = await _historyService.GetHistoryAsync();

            return this.Ok(_mapper.Map<List<CalculatorHistoryDto>>(history));
        }


        [HttpGet("posta1code")]
        public async Task<ActionResult<List<PostalCode>>> PostalCode()
        {
            var postalCodes = await _postalCodeService.GetPostalCodesAsync();

            return this.Ok(_mapper.Map<List<PostalCodeDto>>(postalCodes));
        }
    }
}