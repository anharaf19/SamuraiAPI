using SamuraiAPI.Domain;

namespace SamuraiAPI.DTO
{
    public class SwordReadWithTypeDTO
    {
        public int Id { get; set; }
        public string SwordName { get; set; }

        public int Weight { get; set; }

        public int SamuraiId { get; set; }

        public SwordReadTypeDTO SwordType { get; set; } = new SwordReadTypeDTO();
    }
}
