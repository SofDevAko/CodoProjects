using System;


namespace spookify.Models
{
    public class PlaylistTrack : BaseEntity
    {
        public int PlaylistTrackId{ get; set; }

        public int TrackId { get; set; }

        public Track Track { get; set; }

        public string TrackMBID { get; set; }

        public int PlaylistId { get; set; }

        public Playlist Playlist { get; set; }


        // public PlaylistTrack ()
        // {
        //     Tracks = new List<Track>();
        //     Playlists = new List<Playlist>();
        // }


    }

}