namespace InterestRateCounter.Controllers
{
    using System;
    using System.Threading.Tasks;
    using InterestRateCounter.Common.Enums;
    using InterestRateCounter.Service.ServiceModels;
    using InterestRateCounter.Service.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [Route("api/InterestRate")]
    [ApiController]
    public class InterestRateController : ControllerBase
    {
        private readonly IRateCalculationService _rateCalculationService;

        public InterestRateController(IRateCalculationService rateCalculationService)
        {
            _rateCalculationService = rateCalculationService;
        }

        [Route("", Name = "GetInterestRate")]
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(ResultModel),200)]
        public async Task<IActionResult> Get([FromBody]AgreementModel agreementModel)
        {
            //Fail fast
            if (!IsRequestValid(agreementModel, out string message))
            {
                return BadRequest(message);
            }

            var result = await _rateCalculationService.GetCustomerDataAsync(agreementModel);

            if (result == null) return BadRequest();

            return Ok(result);
        }

        //Would put in a separate service and define validation rules
        private bool IsRequestValid(AgreementModel model, out string message)
        {
            if (!string.IsNullOrEmpty(model.BaseRateCode) || !Enum.GetNames(typeof(BaseRateCode)).Contains(model.BaseRateCode))
            {
                message = "Invalid base rate code";
                return false;
            }

            message = string.Empty;

            return true;
        }
    }
}