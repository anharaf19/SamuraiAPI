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
            CreateMap<SwordTypeDTO, SwordType>();
            CreateMap<SwordCreateWithTypeDTO, Sword>();
            CreateMap<Sword, SwordReadWithTypeDTO>();
            CreateMap<SwordType, SwordReadTypeDTO>();
            CreateMap<SwordElementDTO, SwordElement>();
            CreateMap<SwordElement, SwordElementDTO>();
        }
    }
}
