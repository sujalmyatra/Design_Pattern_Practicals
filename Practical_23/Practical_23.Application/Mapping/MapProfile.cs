using AutoMapper;
using Practical_23.Application.DTOs;
using Practical_23.Domain.Entities;

namespace Practical_23.Application.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Employee, ResponseDto>()
            .ForMember(dest => dest.Department,opt => opt.MapFrom(src => src.DepartmentId.ToString()));
        CreateMap<CreateDto, Employee>();
        CreateMap<UpdateDto, Employee>()
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Joiningdate, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
