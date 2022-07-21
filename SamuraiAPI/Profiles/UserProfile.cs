using AutoMapper;
using SamuraiAPI.Domain;
using SamuraiAPI.DTO;

namespace SamuraiAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DefaultUserDTO, User>();
            CreateMap<User,DefaultUserDTO>();
        }
    }
}
