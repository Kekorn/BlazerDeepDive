using Microsoft.EntityFrameworkCore;
namespace BlazorGoogleAuth.Autentication
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<AppUser> GoogleUsers { get; set; }
    }
}
