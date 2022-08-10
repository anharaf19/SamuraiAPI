using SamuraiAPI.Domain;

namespace SamuraiAPI.DTO
{
    public class AllSamuraiReadDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Sword> Swords { get; set; } = new List<Sword>();
    }
}
