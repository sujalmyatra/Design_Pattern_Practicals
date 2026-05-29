using AutoMapper;
using Practical_23.Application.DTOs;
using Practical_23.Domain.Entities;

namespace Practical_23.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateEmployeeDto, Employee>();

        CreateMap<UpdateEmployeeDto, Employee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.JoiningDate, opt=> opt.Ignore())
            .ForMember(dest => dest.status, opt => opt.Ignore());

        CreateMap<Employee, EmployeeResponseDto>()
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.DepartmentId.ToString()));
    }
}
