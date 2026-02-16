using AutoMapper;
using Assessment9.Models;
using Assessment9.DTOs;

namespace Assessment9.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}
