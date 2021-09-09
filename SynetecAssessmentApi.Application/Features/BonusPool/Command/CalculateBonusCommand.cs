using MediatR;
using SynetecAssessmentApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Features.BonusPool.Command
{
    public class CalculateBonusCommand : IRequest<BonusPoolCalculatorResultDto>
    {
        public CalculateBonusCommand(int totalBonusPoolAmount, int? selectedEmployeeId)
        {
            TotalBonusPoolAmount = totalBonusPoolAmount;
            SelectedEmployeeId = selectedEmployeeId;
        }
        public int TotalBonusPoolAmount { get; set; }
        public int? SelectedEmployeeId { get; set; } = null;
    }
}
