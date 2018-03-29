using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace spookify.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId {get;set;}
        public string Username {get;set;}
        public string Email {get;set;}
        public string Password {get;set;}
        public DateTime DoB {get;set;}
        public string Gender {get;set;}
        // public List<Playlist> UserPlaylists {get;set;}
        public List<Library> UserLibrary {get;set;}

        public User()
        {
            // List<Playlist> UserPlaylist = new List<Playlist>();
            List<Library> UserLibrary = new List<Library>();
        }
        
    }
}
