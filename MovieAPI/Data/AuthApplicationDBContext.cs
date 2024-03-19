
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Data
{
    public class AuthApplicationDBContext : IdentityDbContext<AppUser>
    {
        public AuthApplicationDBContext(DbContextOptions<AuthApplicationDBContext> options) : base(options)
        {
        }


    }
}