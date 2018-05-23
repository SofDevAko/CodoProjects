using Microsoft.EntityFrameworkCore;
// using System.Data.Entity.ModelConfiguration;
// using StaticDotNet.EntityFrameworkCore.ModelConfiguration;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;




namespace spookify.Models
{
    public class SpookifyContext : DbContext
    {
        public SpookifyContext(DbContextOptions<SpookifyContext> options) : base(options) { }
        public DbSet<Artist> Artists {get;set;}
        public DbSet<Album> Albums {get;set;}
        public DbSet<Track> Tracks {get;set;}
        public DbSet<User> Users {get;set;}
        public DbSet<Playlist> Playlists {get;set;}

        public DbSet<PlaylistTrack> PlaylistTracks { get; set; }


        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Track>()
        //                 .HasKey(x => x.TrackId);

        //     modelBuilder.Entity<Playlist>()
        //                 .HasKey(x => x.PlaylistId);

        //     modelBuilder.Entity<PlaylistTrack>()
        //                 .HasKey(x => new {x.TrackId, x.PlaylistId});
        // }

    }
}