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
        
        public string ArtistListeners2 { get; set; }
        public int? ArtistPlaycount { get; set; }
        
        public string ArtistPlaycount2 {get; set; }
        public string ArtistMBID {get; set; }
        public string ArtistURL {get; set; }
        // public ImageSet MainImage { get; set; }
        // public Stats ArtistStats {get; set; }
        
        public List<Tag> Tags { get; set; }

        public List<Album> Albums { get; set; }
        
        public Artist()
        {
            List<Tag> Tags= new List<Tag>(); 
            List<Album> Albums = new List<Album>(); 
        }
    }
}