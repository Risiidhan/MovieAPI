
namespace MovieAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int? ReleasedYear { get; set; }
        public bool? IsMyFavourite { get; set; }
        public int LeadActorID { get; set; }

        //navigation property
        public Actor? LeadActor { get; set; }
    }
}