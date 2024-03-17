using MovieAPI.DTO.actor;
using MovieAPI.Models;

namespace MovieAPI.DTO.movie
{
    public class movieDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? ReleasedYear { get; set; }
        public bool? IsMyFavourite { get; set; }

        //navigation property
        public actorDto? LeadActor { get; set; }

    }
}