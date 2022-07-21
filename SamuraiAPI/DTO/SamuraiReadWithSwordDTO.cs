namespace SamuraiAPI.DTO
{
    public class SamuraiReadWithSwordDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SwordDTO> Swords { get; set; } = new List<SwordDTO>();
    }
}
