using AutoMapper;
using SamuraiAPI.Domain;
using SamuraiAPI.DTO;


namespace SamuraiAPI.Profiles
{
    public class ElementProfile : Profile
    {
    
        public ElementProfile()
        {
            CreateMap<Element, DefaultElementDTO>();
            CreateMap<DefaultElementDTO, Element>();
        }
    }
}
