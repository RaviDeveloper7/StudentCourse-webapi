using AutoMapper;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;
using StudentCourseAPI.Repositories;

namespace StudentCourseAPI.Services
{   
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> repository;
        private readonly IMapper mapper;
                
        public EmployeeService(IRepository<Employee> repository , IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeReadDto>> GetAllAsync()
        {
            var employees = await repository.GetAllAsync(e => e.EmployeeDetail);

            var mappedDtos = mapper.Map<IEnumerable<EmployeeReadDto>>(employees);

            return mappedDtos;
        }

        public async Task<EmployeeReadDto> GetByIdAsync(int id)
        {
            var employee = await repository.GetByIdAsync(id , e=>e.EmployeeDetail);

            if (employee == null) {return null;}

            var mappedDto = mapper.Map<EmployeeReadDto>(employee);

            return mappedDto;
        }

        public async Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto employeeCreateDto)
        {
            var mappedEmployee = mapper.Map<Employee>(employeeCreateDto);

            var createdEmployee = await repository.AddAsync(mappedEmployee);
            
            var mappedDto= mapper.Map<EmployeeReadDto>(createdEmployee);

            return mappedDto;
        }
                    
        public async Task<bool> UpdateAsync(int id, EmployeeUpdateDto employeeUpdateDto)
        {
            var existingEmployee = await repository.GetByIdAsync(id, e => e.EmployeeDetail);

            if (existingEmployee == null) return false;

            mapper.Map(employeeUpdateDto, existingEmployee);

            // Update scalar fields
           // existingEmployee.Name = employeeUpdateDto.Name;

            // Handle nested update
            //if (existingEmployee.EmployeeDetail == null)
            //{
            //    existingEmployee.EmployeeDetail = mapper.Map<EmployeeDetail>(employeeUpdateDto.EmployeeDetail);
            //}
            //else
            //{
            //    existingEmployee.EmployeeDetail.Address = employeeUpdateDto.EmployeeDetail.Address;
            //    existingEmployee.EmployeeDetail.PhoneNumber = employeeUpdateDto.EmployeeDetail.PhoneNumber;
            //}

            var updated = await repository.UpdateAsync(existingEmployee);
            // mapper.Map<EmployeeReadDto>(updated);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await repository.DeleteAsync(id);
            return true;
        }
    }
}