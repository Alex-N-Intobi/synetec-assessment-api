using FluentValidation;
using SynetecAssessmentApi.Application.Features.BonusPool.Command;

namespace SynetecAssessmentApi.Application.Validators
{
    public class CalculateBonusCommandValidator : AbstractValidator<CalculateBonusCommand>
    {
        public CalculateBonusCommandValidator()
        {
            RuleFor(p => p.SelectedEmployeeId)
                .Must(item => {
                    if (item.HasValue)
                    {
                        return true;
                    }
                    return false;
                })
                .WithMessage("SelectedEmployeeId is not specified found!");

            RuleFor(p => p.TotalBonusPoolAmount);
        }
    }
}
