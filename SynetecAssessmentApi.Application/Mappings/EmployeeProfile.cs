using AutoMapper;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;

namespace SynetecAssessmentApi.Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dst => dst.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dst => dst.Fullname, opt => opt.MapFrom(src => src.Fullname))
                .ForMember(dst => dst.JobTitle, opt => opt.MapFrom(src => src.JobTitle))
                .ForMember(dst => dst.Salary, opt => opt.MapFrom(src => src.Salary));
        }
    }
}
