using Microsoft.EntityFrameworkCore;
namespace spookify.Models
{
    public class SpookifyContext : DbContext
    {
        public SpookifyContext(DbContextOptions<SpookifyContext> options) : base(options) { }
        public DbSet<Artist> Artists {get;set;}
        public DbSet<Album> Albums {get;set;}
        public DbSet<Track> Tracks {get;set;}
        public DbSet<User> Users {get;set;}
        public DbSet<Library> Libraries {get;set;}
        
    }
}