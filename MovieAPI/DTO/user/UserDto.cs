using System.ComponentModel.DataAnnotations;

namespace MovieAPI.DTO.user
{
    public class UserDto
    {
        [Required]
        public string? Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        
        [Required]
        public string? Password { get; set; }

        public string[] Roles { get; set; }
    }
}