namespace SynetecAssessmentApi.Dtos
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {

        }
        public EmployeeDto(string fullname, string jobTitle, int salary, DepartmentDto department)
        {
            Fullname = fullname;
            JobTitle = jobTitle;
            Salary = salary;
            Department = department;
        }

        public string Fullname { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
