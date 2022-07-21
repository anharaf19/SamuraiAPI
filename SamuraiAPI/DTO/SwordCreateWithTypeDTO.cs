namespace SamuraiAPI.DTO
{
    public class SwordCreateWithTypeDTO
    {
        public string SwordName { get; set; }
        public int Weight { get; set; }
        public int SamuraiId
        {
            get; set;
        }
        public SwordTypeDTO SwordType { get; set; } = new SwordTypeDTO();
    }
}
