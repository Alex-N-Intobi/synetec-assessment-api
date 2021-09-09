
using System.Threading.Tasks;
using Xunit;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Application.Features.BonusPool.Command;
using Bogus;
using SynetecAssessmentApi.Controllers;
using MediatR;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace SynetecAssessment.Api.UnitTests
{

    public class BonusPoolUnitControllerTests
    {
        [Fact]
        public async Task TestCalculateBonus()
        {
            var fixture = new BonusPoolUnitControllerFixture();
            var createCalculateBonusCommand = fixture.CreateCalculateBonusCommandFaker.Generate();
            var bonusPoolCalculatorResultDto = fixture.BonusPoolCalculatorResultDtoFaker.Generate();
            fixture
                .MediatorMock
                .Setup(x => x.Send(It.IsAny<CalculateBonusCommand>(), default)
                ).ReturnsAsync(
                    new BonusPoolCalculatorResultDto()
                    {
                        Amount = bonusPoolCalculatorResultDto.Amount,
                        Employee = bonusPoolCalculatorResultDto.Employee,
                    }
                );

            var bonusPoolController = fixture.GetBonusPoolController();

            var result = await bonusPoolController.CalculateBonus(
                createCalculateBonusCommand);

            var okResult = (BonusPoolCalculatorResultDto)((ObjectResult)result).Value;

            okResult.Amount.Should().Be(bonusPoolCalculatorResultDto.Amount);

        }
    }
    internal class BonusPoolUnitControllerFixture
    {
        public BonusPoolUnitControllerFixture()
        {
            MediatorMock = new Mock<IMediator>();
            CreateCalculateBonusCommandFaker = new Faker<CalculateBonusCommand>()
                .CustomInstantiator(faker =>
                    new CalculateBonusCommand(
                        totalBonusPoolAmount: faker.GetInt(),
                        selectedEmployeeId: faker.GetIntN()));
            DepartmentDtoFaker = new Faker<DepartmentDto>()
                .RuleFor(x => x.Title, f => f.GetString())
                 .RuleFor(x => x.Description, f => f.GetString());

            EmployeeDtoFaker = new Faker<EmployeeDto>()
                .CustomInstantiator(faker => new EmployeeDto(
                    fullname: faker.GetString(),
                    jobTitle: faker.GetString(),
                    salary: faker.GetInt(),
                    department: DepartmentDtoFaker.Generate()
                    ));

            BonusPoolCalculatorResultDtoFaker = new Faker<BonusPoolCalculatorResultDto>()
                .CustomInstantiator(faker => new BonusPoolCalculatorResultDto(
                    amount: faker.GetInt(), 
                    EmployeeDtoFaker.Generate()));
        }
        public Mock<IMediator> MediatorMock { get; set; }
        public Faker<EmployeeDto> EmployeeDtoFaker { get; set; }
        public Faker<DepartmentDto> DepartmentDtoFaker { get; set; }
        public Faker<BonusPoolCalculatorResultDto> BonusPoolCalculatorResultDtoFaker { get; set; }
        public Faker<CalculateBonusCommand> CreateCalculateBonusCommandFaker { get; set; }
        public BonusPoolController GetBonusPoolController()
        {
            return new BonusPoolController(MediatorMock.Object);
        }
    }
}
