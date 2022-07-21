using SamuraiAPI.Domain;

namespace SamuraiAPI.DTO
{
    public class SwordCreateDTO
    {
        public int Id { set; get; }
        public string SwordName { get; set; }
        public int Weight { get; set; }
        public int SamuraiId
        {
            get; set;
        }



    }
}