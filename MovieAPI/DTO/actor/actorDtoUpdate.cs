using System.ComponentModel.DataAnnotations;
using MovieAPI.Enums;

namespace MovieAPI.DTO.actor
{
    public class actorDtoUpdate
    {
        private const string RequiredSentence = "is required!";
        private const string MinLengthSentence = "must be at least 2 characters!";
        private const string MaxLengthSentence = "must be maximum of 25 characters!";

        [Required(ErrorMessage = $"Name {RequiredSentence}")]
        [MinLength(2, ErrorMessage = $"Name {MinLengthSentence}")]
        [MaxLength(25, ErrorMessage = $"Name {MaxLengthSentence}")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = $"Age {RequiredSentence}")]
        [Range(1, 150, ErrorMessage = "Age must be between 1 and 150!")]
        public int Age { get; set; }

        [Required(ErrorMessage = $"Gender {RequiredSentence}")]
        [EnumDataType(typeof(GenderEnum), ErrorMessage = "Gender should be Male, Female, or Other!")]
        public GenderEnum Gender { get; set; }

        [MinLength(2, ErrorMessage = $"Nationality {MinLengthSentence}")]
        [MaxLength(25, ErrorMessage = $"Nationality {MaxLengthSentence}")]
        public string? Nationality { get; set; }
    }
}