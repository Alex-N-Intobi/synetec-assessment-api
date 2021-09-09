using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SynetecAssessmentApi.Application.Mappings;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Infrastructure.Services.BonusPool;
using SynetecAssessmentApi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestService.Tests
{
    public class BonusPoolUnitTest
    {
        private readonly IMapper _mapper;
        private DbContextOptions<AppDbContext> _dbContextOptions;

        public BonusPoolUnitTest()
        {
            var mapperConfig = new MapperConfiguration(configuration =>
            {
                IEnumerable<Type>? profiles = typeof(EmployeeProfile).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
                if (profiles != null)
                {
                    foreach (var profile in profiles)
                    {
                        configuration.AddProfile(Activator.CreateInstance(profile) as Profile);
                    }
                }
            });
            var mapper = mapperConfig.CreateMapper();
            _mapper = mapper;
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "HrDb-UnitTests").Options;
            using (var context = new AppDbContext(_dbContextOptions))
            {
                SeedData(context);
            }
        }
        [Fact]
        public void CalculateAsync_InputCorectData_ReturnData()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                // Arrange
                BonusPoolService service = new BonusPoolService(context, _mapper);

                // Act
                var result = service.CalculateAsync(10, 2).Result;

                // Assert
                Assert.NotNull(result);
            }
        }


        [Fact]
        public void CalculateAsync_InputIncorectData_ReturnData()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                // Arrange
                BonusPoolService service = new BonusPoolService(context, _mapper);

                // Act
                var result = service.CalculateAsync(0, 0).Result;

                // Assert
                Assert.Null(result);
            }
        }

        private static void SeedData(AppDbContext context)
        {
            var departments = new List<Department>
            {
                new Department(1, "Finance", "The finance department for the company"),
                new Department(2, "Human Resources", "The Human Resources department for the company"),
                new Department(3, "IT", "The IT support department for the company"),
                new Department(4, "Marketing", "The Marketing department for the company")
            };

            var employees = new List<Employee>
            {
                new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1),
                new Employee(2, "Janet Jones", "HR Director", 90000, 2),
                new Employee(3, "Robert Rinser", "IT Director", 95000, 3),
                new Employee(4, "Jilly Thornton", "Marketing Manager (Senior)", 55000, 4),
                new Employee(5, "Gemma Jones", "Marketing Manager (Junior)", 45000, 4),
                new Employee(6, "Peter Bateman", "IT Support Engineer", 35000, 3),
                new Employee(7, "Azimir Smirkov", "Creative Director", 62500, 4),
                new Employee(8, "Penelope Scunthorpe", "Creative Assistant", 38750, 4),
                new Employee(9, "Amil Kahn", "IT Support Engineer", 36000, 3),
                new Employee(10, "Joe Masters", "IT Support Engineer", 36500, 3),
                new Employee(11, "Paul Azgul", "HR Manager", 53000, 2),
                new Employee(12, "Jennifer Smith", "Accountant (Junior)", 48000, 1),
            };
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(departments);
            }
            if (!context.Employees.Any())
            {
                context.Employees.AddRange(employees);
            }
            context.SaveChanges();
        }

    }




}
