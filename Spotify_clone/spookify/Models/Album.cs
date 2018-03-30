using System.Collections.Generic;
using System;
using Newtonsoft.Json;
namespace spookify.Models
{
    public class Album : BaseEntity
    {
        public int AlbumId { get; set; }

        public string AlbumMBID { get; set; }

        public string AlbumName { get; set; }

        public string AlbumArtist { get; set; }
        public int ArtistId { get; set; }

        public string AlbumURL { get; set; }
        
        public string AlbumImage { get; set; }

        public DateTime AlbumReleaseDate { get; set; }

        public string AlbumListeners { get; set; }
        
        public string AlbumSummary {get; set; }

        public string AlbumPlaycount { get; set; }

        public List<Track> AlbumTracks {get; set; }

        public List<string> Tags { get; set; }

        public Album()
        {
            Tags = new List<string>(); 
            List<Track> AlbumTracks = new List<Track>();
        }
    }
}
