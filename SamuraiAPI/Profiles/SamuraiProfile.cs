using AutoMapper;
using SamuraiAPI.Domain;
using SamuraiAPI.DTO;

namespace SamuraiAPI.Profiles
{
    public class SamuraiProfile : Profile
    {
        public SamuraiProfile()
        {
            CreateMap<Samurai, DefaultSamuraiDTO>();
            CreateMap<SwordCreateDTO, Sword>();
            CreateMap<DefaultSamuraiDTO, Samurai>();

            CreateMap<SamuraiCreateWithSwordDTO, Samurai>().ReverseMap(); 
            CreateMap<Samurai, SamuraiReadWithSwordDTO>().ReverseMap(); ;
        }
    }
}
