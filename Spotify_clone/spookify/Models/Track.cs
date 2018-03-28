using System.Collections.Generic;
using System;
namespace spookify.Models
{
    public class Track : BaseEntity
    {
        public int TrackId { get; set; }
        public int TrackRank { get; set; }
        public string TrackName { get; set; }
        public string TrackURL { get; set; }
        public int TrackMBID { get; set; }
        public int TrackDuration {get; set; }
        public int TrackListeners { get; set; }
        public int TrackPlaycount { get; set; }
    }
}