using MovieAPI.DTO.actor;

namespace MovieAPI.DTO.movie
{
    public class movieDtoByID
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? ReleasedYear { get; set; }
        public bool? IsMyFavourite { get; set; }

        //navigation property
        public actorDtoByID? LeadActor { get; set; }
    }
}