using CarZone_Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarZone_Backend.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
      public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
        
    }
}
