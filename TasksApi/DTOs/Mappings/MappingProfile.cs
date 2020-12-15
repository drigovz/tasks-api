using AutoMapper;
using TasksApi.Models;

namespace TasksApi.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Tasks, TasksDTO>().ReverseMap();
        }
    }
}