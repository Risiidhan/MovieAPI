using MovieAPI.DTO.actor;

namespace MovieAPI.DTO.movie
{
    public class movieDtoByID
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? ReleasedYear { get; set; }
        public bool? IsMyFavourite { get; set; }
        public ICollection<actorDtoByID> Actors { get; set; }

    }
}