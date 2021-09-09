using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Application.Interfaces.Services.BonusPool;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Infrastructure.Services.BonusPool
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public BonusPoolService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            return await _dbContext.Employees.Include(e => e.Department)
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            Employee employee = await _dbContext.Employees.Include(e => e.Department).FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);

            if (employee == null)
            {
                return null;
            }

            //get the total salary budget for the company
            int totalSalary = (int)_dbContext.Employees.Sum(item => item.Salary);

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);

            return new BonusPoolCalculatorResultDto
            {
                Employee = _mapper.Map<EmployeeDto>(employee),
                Amount = bonusAllocation,
            };
        }
    }
}
