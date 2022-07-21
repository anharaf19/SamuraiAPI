using SamuraiAPI.Domain;
using SamuraiAPI.DTO;
using AutoMapper;

namespace SamuraiAPI.Profiles
{
    public class SwordProfile : Profile
    {
        public SwordProfile()
        {
            CreateMap<Sword, DefaultSwordDTO>();
            CreateMap<Sword, SwordDTO>();
            CreateMap<DefaultSwordDTO, Sword>();
            CreateMap<Sword, SwordReadTypeDTO>();
            CreateMap<Sword, SwordReadWithTypeDTO>();

            CreateMap<SwordTypeDTO, SwordType>();
            CreateMap<SwordCreateWithTypeDTO, Sword>();


        }
    }
}
