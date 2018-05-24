using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
namespace spookify.Models
{
    public class Track : BaseEntity
    {
        [Key]
        public int TrackId { get; set; }

        public int TrackRank { get; set; }

        public string TrackName { get; set; }

        public string TrackURL { get; set; }

        public string TrackMBID { get; set; }

        public int TrackDuration {get; set; }

        public int TrackListeners { get; set; }

        public int TrackPlaycount { get; set; }

        public string TrackImage { get; set; }

        public string ArtistName { get; set; }

        public string ArtistMBID { get; set; }

        public string ArtistURL { get; set; }

        public ICollection<PlaylistTrack> PlaylistTracks { get; set; }
        // public List<PlaylistTrack> Playlists { get; set; }

        // public List<Playlist> TrackPlaylists { get; set; }


        // public Track ()
        // {
        //     PlaylistTracks = new List<PlaylistTrack>();
        //     // TrackPlaylists = new List<Playlist>();

        // }

    }
}