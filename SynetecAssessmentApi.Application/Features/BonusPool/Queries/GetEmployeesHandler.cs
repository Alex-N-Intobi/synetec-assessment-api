using MediatR;
using SynetecAssessmentApi.Application.Interfaces.Services.BonusPool;
using SynetecAssessmentApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Features.BonusPool.Queries
{
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, IEnumerable<EmployeeDto>>
    {
        private readonly IBonusPoolService _bonusService;

        public GetEmployeesHandler(IBonusPoolService bonusService)
        {
            _bonusService = bonusService;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _bonusService.GetEmployeesAsync();
        }
    }
}
