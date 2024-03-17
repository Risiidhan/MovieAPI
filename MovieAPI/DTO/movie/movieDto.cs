using MovieAPI.Models;

namespace MovieAPI.DTO.movie
{
    public class movieDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? ReleasedYear { get; set; }
        public bool? IsMyFavourite { get; set; }
        public List<int> ActorsList { get; set; } = new List<int>();


        //navigation property
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();

    }
}