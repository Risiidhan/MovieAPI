
namespace MovieAPI.Models
{
    public class Movie
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