namespace InterestRateCounterTests.Controllers
{
    using InterestRateCounter.Controllers;
    using InterestRateCounter.Service.ServiceModels;
    using InterestRateCounter.Service.Services;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Moq.AutoMock;
    using System.Threading.Tasks;
    using Xunit;

    public class InterestRateControllerTests
    {
        private InterestRateController _classUnderTest;
        private AutoMocker _mocker;

        public InterestRateControllerTests()
        {
            _mocker = new AutoMocker();
            _classUnderTest = _mocker.CreateInstance<InterestRateController>();
        }

        private void SetUpHappyPath()
        {
            _mocker = new AutoMocker();

            var mockedRateService = new Mock<IRateCalculationService>();
            mockedRateService.Setup(x => x.GetCustomerDataAsync(It.IsAny<AgreementModel>())).ReturnsAsync(new ResultModel());
            _mocker.Use(mockedRateService.Object);

            _classUnderTest = _mocker.CreateInstance<InterestRateController>();
        }

        [Fact]
        public async Task Put_Model_Is_Null_Expected_BadRequest()
        {
            var result = await _classUnderTest.Put(null);

            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(badRequestResult);
        }

        [Fact]
        public async Task Put_Null_BaseRateCode_Expected_BadRequest()
        {
            var result = await _classUnderTest.Put(new AgreementModel() { BaseRateCode = null });

            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(badRequestResult);
        }

        [Fact]
        public async Task Put_Empty_BaseRateCode_Expected_BadRequest()
        {
            var result = await _classUnderTest.Put(new AgreementModel() { BaseRateCode = string.Empty });

            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(badRequestResult);
        }

        [Fact]
        public async Task Put_Valid_Base_Rate_Code_Expected_Ok()
        {
            SetUpHappyPath();

            var result = await _classUnderTest.Put(new AgreementModel() { BaseRateCode = "VILIBOR1m" });

            var OkObjectResult = result as OkObjectResult;

            Assert.NotNull(OkObjectResult);
        }
    }

}
