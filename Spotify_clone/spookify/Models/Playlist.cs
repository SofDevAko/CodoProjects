using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace spookify.Models
{
    public class Playlist : BaseEntity
    {
        [Key]
        public int PlaylistId { get; set; }

        public string PlaylistName { get; set; }

        public string PlaylistImage { get; set; }

        public string PlaylistDescription { get; set; }

        // public List<PlaylistTrack> Tracks { get; set; }


        public ICollection<PlaylistTrack> PlaylistTracks { get; set; }

        public int UserId { get; set; }

        // public Playlist ()
        // {
        //     Tracks = new List<PlaylistTrack>();
        //     PlaylistTracks = new List<Track>();
        // }


    }

}