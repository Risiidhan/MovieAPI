namespace MovieAPI.DTO.actor
{
    public class actorDtoCreate
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string? Nationality { get; set; }
    }
}