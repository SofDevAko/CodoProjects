using System.Collections.Generic;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;


namespace spookify.Models
{
    public class Artist : BaseEntity
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistBio { get; set; }
        public int ArtistListeners { get; set; }
        public int? ArtistPlaycount { get; set; }
        public int ArtistMBID {get; set; }
        public string ArtistURL {get; set; }
    }
}