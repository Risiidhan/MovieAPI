using System.ComponentModel.DataAnnotations;


namespace MovieAPI.DTO.movie
{
    public class movieDtoUpdate
    {
       private const string RequiredSentence = "is required!";
        private const string MinLengthSentence = "must be at least 2 characters!";
        private const string MaxLengthSentence = "must be maximum of 25 characters!";


        [Required(ErrorMessage = $"Name {RequiredSentence}")]
        [MinLength(2, ErrorMessage = $"Name {MinLengthSentence}")]
        [MaxLength(25, ErrorMessage = $"Name {MaxLengthSentence}")]
        public required string Name { get; set; }
        public int? ReleasedYear { get; set; }
        public bool? IsMyFavourite { get; set; }
        public List<int> ActorIds { get; set; }

    }
}