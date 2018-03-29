using System.Collections.Generic;
using System;
using Newtonsoft.Json;
namespace spookify.Models
{
    public class Album : BaseEntity
    {
        public int AlbumId { get; set; }

        [JsonProperty("mbid")]
        public int AlbumMBID { get; set; }
        public string AlbumName { get; set; }
        public string AlbumArtist { get; set; }
        public int ArtistId { get; set; }

        public string AlbumURL { get; set; }
        public DateTime AlbumReleaseDate { get; set; }
        public int AlbumListeners { get; set; }
        public int AlbumPlaycount { get; set; }
        public List<Track> AlbumTracks {get; set; }
    }
}
