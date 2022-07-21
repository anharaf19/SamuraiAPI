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
            CreateMap<DefaultSwordDTO, Sword>();

            CreateMap<SwordTypeDTO, SwordType>();
            CreateMap<SwordCreateWithTypeDTO, Sword>();


        }
    }
}
