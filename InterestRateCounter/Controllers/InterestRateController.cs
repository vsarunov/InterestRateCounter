namespace InterestRateCounter.Controllers
{
    using System;
    using System.Threading.Tasks;
    using InterestRateCounter.Common.Enums;
    using InterestRateCounter.Service.ServiceModels;
    using InterestRateCounter.Service.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    [Route("api/InterestRate")]
    [ApiController]
    public class InterestRateController : ControllerBase
    {
        private readonly IRateCalculationService _rateCalculationService;
        private readonly ILogger<InterestRateController> _log;

        public InterestRateController(IRateCalculationService rateCalculationService, ILogger<InterestRateController> log)
        {
            _rateCalculationService = rateCalculationService;
            _log = log;
        }

        [Route("", Name = "GetInterestRate")]
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ResultModel), 200)]
        public async Task<IActionResult> Put([FromBody]AgreementModel agreementModel)
        {
            _log.LogInformation("Received request for interest rate calculation");
            //Fail fast
            if (!IsRequestValid(agreementModel, out string message))
            {
                _log.LogWarning(message);
                return BadRequest(message);
            }

            _log.LogInformation($"Starting interest rate calculations with base ratecode {agreementModel.BaseRateCode}");
            var result = await _rateCalculationService.GetCustomerDataAsync(agreementModel);

            if (result == null)
            {
                _log.LogWarning("Failed to calculated interest rate result is null");
                return BadRequest();
            }

            return Ok(result);
        }

        //Would put in a separate service and define validation rules
        // Is a 0 or a minus margin,amount or duration a valid post?
        private bool IsRequestValid(AgreementModel model, out string message)
        {
            if (model == null)
            {
                message = "Model cannot be null";
                return false;
            }

            if (string.IsNullOrEmpty(model.BaseRateCode) || !Enum.GetNames(typeof(BaseRateCode)).Contains(model.BaseRateCode))
            {
                message = "Invalid base rate code";
                return false;
            }

            message = string.Empty;

            return true;
        }
    }
}