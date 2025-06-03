using AutoMapper;
using Moq;
using StudentCourseAPI.Models;
using StudentCourseAPI.Repositories;
using StudentCourseAPI.Services;
using StudentCourseAPI.DTOs;

namespace MyApi.Tests.Controllers
{
    public class DepartmentServiceTests
    {
        private readonly Mock<IRepository<Department>> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DepartmentService _service;


        public DepartmentServiceTests()
        {
            _mockRepo = new Mock<IRepository<Department>>();
            _mockMapper = new Mock<IMapper>();
            _service = new DepartmentService(_mockRepo.Object, _mockMapper.Object);
        }


        [Fact]
        public async Task GetAllDeparmentAsync_ReturnsMappedDTOs()
        {
            var departments = new List<Department>
            {
                new Department{Id =1, Name = "Item 1" , Location = "Hyderabad" },
                new Department{Id=2 , Name = "Item 2", Location = "vizag"}
            };

            var departmentDTOs = new List<DepartmentReadDto>
            {
                new DepartmentReadDto{ Id =1 , Name = "Item 1", Location = "Hyderabad" },
                new DepartmentReadDto{ Id =2 , Name = "Item 2", Location = "vizag" }

            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(departments);
            _mockMapper.Setup(m => m.Map<IEnumerable<DepartmentReadDto>>(departments)).Returns(departmentDTOs);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.Collection(result,
                item =>
                {
                    Assert.Equal("Item 1", item.Name);
                    Assert.Equal("Hyderabad", item.Location);
                },
                item =>
                {
                    Assert.Equal("Item 2", item.Name);
                    Assert.Equal("vizag", item.Location);
                });
        }

        [Fact]
        public async Task GetDepartmentByIdAsync_ValidId_ReturnsMappedDTO()
        {

            var department = new Department{ Id =1, Name = "Item1", Location = "Hyderabad" };

            var departmentDTO = new DepartmentReadDto { Id = 1, Location = "Hyderabad", Name = "Item1" };


            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(department);

            _mockMapper.Setup(m => m.Map<DepartmentReadDto>(department)).Returns(departmentDTO);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);

            Assert.Equal("Item1", result.Name);
        }

        [Fact]
        public async Task CreateDepartmentByIdAsync_ReturnsCreatedDTO()
        {
            var createdDTO = new DepartmentCreateDto { Name = "ash" , Location ="kalakal" };

            var department = new Department {Id=1, Name = "ash", Location = "kalakal" };

            var mappedDTO = new DepartmentReadDto { Id = 1, Name = "ash", Location = "kalakal" };   

            _mockMapper.Setup(m => m.Map<Department>(createdDTO)).Returns(department);

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Department>())).ReturnsAsync(department);
                
            _mockMapper.Setup(m => m.Map<DepartmentReadDto>(department)).Returns(mappedDTO);
                
            var result = await _service.CreateAsync(createdDTO);

            Assert.NotNull(result);

            Assert.Equal("ash", result.Name);   
                
            Assert.Equal("kalakal", result.Location);    
        }

        [Fact]
        public async Task UpdateDepartmentByIdAsync_ReturnsUpdatedDTO()
        {
            var departmentId = 1;
            var updatedDTO = new DepartmentUpdateDto { Location = "hyd", Name = "ash" };
            var mappedDTO = new Department {Id=1, Location = "hyd", Name = "ash" };
            var existingDepartment = new Department {Id=1, Location = "hyd", Name = "ash" };

            _mockRepo.Setup(r => r.GetByIdAsync(departmentId)).ReturnsAsync(existingDepartment);
          
            _mockMapper.Setup(m => m.Map(updatedDTO, existingDepartment)).Returns(existingDepartment);

            var result = await _service.UpdateAsync(departmentId, updatedDTO);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteDepartmentByIdAsync_ValidId_ReturnsTrue()
        {
            var departmentId = 1;
            var existingDepartment = new Department { };

         //   _mockRepo.Setup(r => r.GetByIdAsync(departmentId)).Returns(existingDepartment);
        }
    }   
}   