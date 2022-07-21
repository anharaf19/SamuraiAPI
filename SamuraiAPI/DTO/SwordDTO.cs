namespace SamuraiAPI.DTO
{
    public class SwordDTO
    {
        public int Id { get; set; }
        public string SwordName { get; set; }
        public int Weight { get; set; }
        public int SamuraiId
        {
            get; set;
        }
    }
}
