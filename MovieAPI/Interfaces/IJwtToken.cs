using MovieAPI.Models;

namespace MovieAPI.Interfaces
{
    public interface IJwtToken
    {
        string GenerateToken(AppUser user);
    }
}