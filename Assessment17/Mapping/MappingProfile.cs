using AutoMapper;
using Assessment17.DTOs;
using Assessment17.Models;

namespace Assessment17.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Department, DepartmentReadDto>();
        CreateMap<DepartmentCreateDto, Department>();
        CreateMap<DepartmentUpdateDto, Department>();

        CreateMap<Project, ProjectReadDto>();
        CreateMap<ProjectCreateDto, Project>();
        CreateMap<ProjectUpdateDto, Project>();
    }
}