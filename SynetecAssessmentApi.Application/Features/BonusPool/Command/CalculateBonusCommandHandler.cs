using MediatR;
using SynetecAssessmentApi.Application.Common.Exceptions;
using SynetecAssessmentApi.Application.Interfaces.Services.BonusPool;
using SynetecAssessmentApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Features.BonusPool.Command
{
    public class CalculateBonusCommandHandler : IRequestHandler<CalculateBonusCommand, BonusPoolCalculatorResultDto>
    {
        private readonly IBonusPoolService _bonusService;

        public CalculateBonusCommandHandler(IBonusPoolService bonusService)
        {
            _bonusService = bonusService;
        }

        public async Task<BonusPoolCalculatorResultDto> Handle(CalculateBonusCommand request, CancellationToken cancellationToken)
        {
            var user = await _bonusService.CalculateAsync(request.TotalBonusPoolAmount, request.SelectedEmployeeId.Value);
            if (user == null)
            {
                throw new NotFoundEmployeeException("Employee not found!");
            }
            return user;
        }
    }
}