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
            CreateMap<DepartmentUpdateDto, Department>();

        }
    }
}