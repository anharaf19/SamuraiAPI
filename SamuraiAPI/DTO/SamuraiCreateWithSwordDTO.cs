using SamuraiAPI.DTO;

namespace SamuraiAPI.DTO
{
    public class SamuraiCreateWithSwordDTO
    {
        public string Name { get; set; }

        public List<SwordCreateDTO> Swords { get; set; } = new List<SwordCreateDTO>();
    }
}
