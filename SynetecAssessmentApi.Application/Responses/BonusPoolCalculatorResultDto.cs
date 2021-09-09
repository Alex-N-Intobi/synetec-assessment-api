namespace SynetecAssessmentApi.Dtos
{
    public class BonusPoolCalculatorResultDto
    {
        public BonusPoolCalculatorResultDto()
        {

        }
        public BonusPoolCalculatorResultDto(int amount, EmployeeDto employee)
        {
            Amount = amount;
            Employee = employee;
        }
        public int Amount { get; set; }
        public EmployeeDto Employee { get; set; }
    }
}
