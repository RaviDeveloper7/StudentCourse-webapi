using AutoMapper;
using StudentCourseAPI.DTOs;
using StudentCourseAPI.Models;

namespace StudentCourseAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
                    
            CreateMap<Department, DepartmentReadDto>();
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>().ForAllMembers(opts=>opts.Condition((src , dest ,srcMember)=>srcMember != null));

            CreateMap<Employee, EmployeeReadDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            //CreateMap<EmployeeUpdateDto, Employee>(); 

            CreateMap<EmployeeDetail, EmployeeDetailReadDto>(); 
            CreateMap<EmployeeDetailCreateDto, EmployeeDetail>();
            // CreateMap<EmployeeDetailUpdateDto, EmployeeDetail>();

            CreateMap<EmployeeUpdateDto, Employee>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<EmployeeDetailUpdateDto, EmployeeDetail>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}