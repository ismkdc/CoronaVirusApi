using AutoMapper;
using CoronaVirusApi.Models;
using CoronaVirusApi.Models.DTO;

namespace CoronaVirusApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CurrentUserDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<InfectedDTO, Infected>();
            CreateMap<Infected, InfectedDTO>();
        }
    }
}
