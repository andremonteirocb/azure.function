using AutoMapper;
using Domain.Entities;
using Service.DTO;

namespace Service.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
