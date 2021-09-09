using MediatR;
using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Application.Features.BonusPool.Command;
using SynetecAssessmentApi.Application.Features.BonusPool.Queries;
using SynetecAssessmentApi.Application.Interfaces.Services.BonusPool;
using SynetecAssessmentApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusPoolController : Controller
    {
        private readonly IMediator _mediatorInstance;
        public BonusPoolController(IMediator mediatorInstance)
        {
            _mediatorInstance = mediatorInstance;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), 200)]
        [ProducesResponseType(typeof(GetEmployeesQuery), 418)]
        public async Task<IActionResult> GetAll()
        {
            return Ok((await _mediatorInstance.Send(new GetEmployeesQuery())));
        }
        [HttpPost()]
        [ProducesResponseType(typeof(BonusPoolCalculatorResultDto), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusCommand request)
        {
            return Ok(await _mediatorInstance.Send(request));
        }
    }
}
