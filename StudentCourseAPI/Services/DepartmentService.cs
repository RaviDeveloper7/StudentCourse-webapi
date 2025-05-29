using AutoMapper;
using StudentCourseAPI.Data;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;
using StudentCourseAPI.Repositories;

namespace StudentCourseAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        public readonly IRepository<Department> _repository;  
        public readonly IMapper _mapper;

        public DepartmentService(IRepository<Department> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentReadDto>> GetAllAsync()
        {
            var departments = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<DepartmentReadDto>>(departments);
        }

        public async Task<DepartmentReadDto> GetByIdAsync(int id)   
        {
           var department = await _repository.GetByIdAsync(id);



            return _mapper.Map<DepartmentReadDto>(department);
        }


        public async Task<DepartmentReadDto> CreateAsync( DepartmentCreateDto _departmentCreateDto)
        {
            var _department= _mapper.Map<Department>(_departmentCreateDto);

            var department = await _repository.AddAsync(_department);

            return _mapper.Map<DepartmentReadDto>(department);

        }

        public async Task<bool> UpdateAsync(int id, DepartmentUpdateDto departmentUpdateto)
        {
            var existingDepartment = await _repository.GetByIdAsync(id);

            if (existingDepartment == null)
                return false;

            _mapper.Map(departmentUpdateto , existingDepartment);

            await _repository.UpdateAsync(existingDepartment);

            return true;
        }

        public async Task<bool> DeleteAsync(int id) 
        {
            var department = await _repository.GetByIdAsync(id);

            if(department == null)
                return false;

            await _repository.DeleteAsync(id);

            return true;

        }
    }
}