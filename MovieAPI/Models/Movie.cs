
namespace MovieAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? ReleasedYear { get; set; }
        public bool? IsMyFavourite { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; }
    }
}