using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MWalks.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            DataSeeder.SeedRoles(builder);
        }

    }
}
