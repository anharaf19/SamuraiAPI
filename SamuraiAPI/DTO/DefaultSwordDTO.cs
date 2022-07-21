using SamuraiAPI.Domain;

namespace SamuraiAPI.DTO
{
    public class DefaultSwordDTO
    {
        //public int Id { get; set; }
        //public string SwordName { get; set; }

        //public string Weight { get; set; }

        ////public Samurai Samurai { get; set; }

        //public int SamuraiId { get; set; }

        //public List<ElementPostDTO> Elements { get; set; } = new List<ElementPostDTO>();

        //public SwordTypeDTO SwordType { get; set; }
        public int Id { get; set; }
        public string SwordName { get; set; }
        public int Weight { get; set; }
        public int SamuraiId
        {
            get; set;
        }
    }
}
