using MediatR;
using SynetecAssessmentApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Application.Features.BonusPool.Queries
{
    public class GetEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>
    {
    }
}
