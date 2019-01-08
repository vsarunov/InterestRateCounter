using InterestRateCounter.Service.ServiceModels;
using InterestRateCounter.Service.Services;
using InterestRateCounter.Service.Services.Implementations;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InterestRateCounter.Services.Tests.Services
{
    public class RateCalculationServiceTest
    {
        private RateCalculationService _classUnderTest;

        private readonly Mock<ICustomerService> _mockedICustomerService;
        private readonly Mock<IBaseRateService> _mockedIBaseRateService;
        private readonly Mock<IAgreementService> _mockedIAgreementService;

        public RateCalculationServiceTest()
        {
            var _mocker = new AutoMocker();
            _mockedICustomerService = new Mock<ICustomerService>();
            _mockedIBaseRateService = new Mock<IBaseRateService>();
            _mockedIAgreementService = new Mock<IAgreementService>();

            _mocker.Use(_mockedICustomerService.Object);
            _mocker.Use(_mockedIBaseRateService.Object);
            _mocker.Use(_mockedIAgreementService.Object);

            _classUnderTest = _mocker.CreateInstance<RateCalculationService>();
        }

        [Fact]
        public async Task GetCustomerDataAsync_Customer_Not_Found_Expected_Null()
        {
            CustomerModel fakeCustomer = null;
            _mockedICustomerService.Setup(x => x.GetCustomersByIdAsync(It.IsAny<long>())).ReturnsAsync(fakeCustomer);

            var result = await _classUnderTest.GetCustomerDataAsync(new AgreementModel());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCustomerDataAsync_CalculateNewInterestRate_BaseRate_IsNull_Expected_No_New_InterestRate()
        {
            CustomerModel fakeCustomer = new CustomerModel();
            _mockedICustomerService.Setup(x => x.GetCustomersByIdAsync(It.IsAny<long>())).ReturnsAsync(fakeCustomer);

            decimal? fakeBaseRate = null;

            _mockedIBaseRateService.Setup(x => x.GetBaseRateByCodeAsync(It.IsAny<string>())).ReturnsAsync(fakeBaseRate);
            _mockedIAgreementService.Setup(x => x.SaveNewAgreementAsync(It.IsAny<AgreementModel>())).ReturnsAsync(new AgreementModel());
            var result = await _classUnderTest.GetCustomerDataAsync(new AgreementModel());

            Assert.NotNull(result);
            Assert.NotNull(result.InterestRates);
            Assert.Null(result.InterestRates.CurrentInterestRate);
            Assert.Null(result.InterestRates.NewInterestRate);
            Assert.Null(result.InterestRates.InterestRateDifference);
        }

        [Fact]
        public async Task GetCustomerDataAsync_CalculateNewInterestRate_BaseRate_IsValid_Expected_New_InterestRate()
        {
            CustomerModel fakeCustomer = new CustomerModel();
            _mockedICustomerService.Setup(x => x.GetCustomersByIdAsync(It.IsAny<long>())).ReturnsAsync(fakeCustomer);

            decimal? fakeBaseRate = 2;

            _mockedIBaseRateService.Setup(x => x.GetBaseRateByCodeAsync(It.IsAny<string>())).ReturnsAsync(fakeBaseRate);
            _mockedIAgreementService.Setup(x => x.SaveNewAgreementAsync(It.IsAny<AgreementModel>())).ReturnsAsync(new AgreementModel());
            var result = await _classUnderTest.GetCustomerDataAsync(new AgreementModel() { Margin = 1 });

            Assert.NotNull(result);
            Assert.NotNull(result.InterestRates);
            Assert.Null(result.InterestRates.CurrentInterestRate);
            Assert.NotNull(result.InterestRates.NewInterestRate);
            Assert.Equal(3, result.InterestRates.NewInterestRate);
            Assert.Null(result.InterestRates.InterestRateDifference);
        }

        [Fact]
        public async Task GetCustomerDataAsync_GetCurrentInterestRate_Has_Result_Expected_All_InterestRate_Properties()
        {
            CustomerModel fakeCustomer = new CustomerModel()
            {
                Agreements = new List<AgreementModel>()
                {
                    new AgreementModel()
                    {
                        Timestamp=DateTime.Now,
                        Margin=4
                    }
                }
            };
            _mockedICustomerService.Setup(x => x.GetCustomersByIdAsync(It.IsAny<long>())).ReturnsAsync(fakeCustomer);

            decimal? fakeBaseRate = 2;
            decimal? fakeBaseRate2 = 3;
            _mockedIBaseRateService.SetupSequence(x => x.GetBaseRateByCodeAsync(It.IsAny<string>())).ReturnsAsync(fakeBaseRate).ReturnsAsync(fakeBaseRate2);
            _mockedIAgreementService.Setup(x => x.SaveNewAgreementAsync(It.IsAny<AgreementModel>())).ReturnsAsync(new AgreementModel());
            var result = await _classUnderTest.GetCustomerDataAsync(new AgreementModel() { Margin = 1 });

            Assert.NotNull(result);
            Assert.NotNull(result.InterestRates);
            Assert.NotNull(result.InterestRates.CurrentInterestRate);
            Assert.Equal(7, result.InterestRates.CurrentInterestRate);
            Assert.NotNull(result.InterestRates.NewInterestRate);
            Assert.Equal(3, result.InterestRates.NewInterestRate);
            Assert.NotNull(result.InterestRates.InterestRateDifference);
            Assert.Equal(4, result.InterestRates.InterestRateDifference);
        }

        [Fact]
        public async Task GetCustomerDataAsync_LastAgreement_Has_Current_Base_Rate_Returns_Null()
        {
            CustomerModel fakeCustomer = new CustomerModel()
            {
                Agreements = new List<AgreementModel>()
                {
                    new AgreementModel()
                    {
                        Timestamp=DateTime.Now,
                        Margin=4
                    }
                }
            };
            _mockedICustomerService.Setup(x => x.GetCustomersByIdAsync(It.IsAny<long>())).ReturnsAsync(fakeCustomer);

            decimal? fakeBaseRate = 2;
            decimal? fakeBaseRate2 = null;
            _mockedIBaseRateService.SetupSequence(x => x.GetBaseRateByCodeAsync(It.IsAny<string>())).ReturnsAsync(fakeBaseRate).ReturnsAsync(fakeBaseRate2);
            _mockedIAgreementService.Setup(x => x.SaveNewAgreementAsync(It.IsAny<AgreementModel>())).ReturnsAsync(new AgreementModel());
            var result = await _classUnderTest.GetCustomerDataAsync(new AgreementModel() { Margin = 1 });

            Assert.NotNull(result);
            Assert.NotNull(result.InterestRates);
            Assert.Null(result.InterestRates.CurrentInterestRate);
            Assert.NotNull(result.InterestRates.NewInterestRate);
            Assert.Equal(3, result.InterestRates.NewInterestRate);
            Assert.Null(result.InterestRates.InterestRateDifference);
        }

    }
}
