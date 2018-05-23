using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using Newtonsoft.Json.Linq;
using Extensions;
using RestSharp;
using spookify.Models;

namespace spookify.Controllers
{
    public class PlaylistController : Controller
    {
        private SpookifyContext _context;

        public PlaylistController(SpookifyContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("ViewPlaylistPage/{id}")]

        public IActionResult ViewPlaylistPage(int id)
        {

            var onePlaylist = _context.Playlists.Include(d => d.PlaylistTracks).ThenInclude(t => t.Track).SingleOrDefault(w => w.PlaylistId == id);

            ViewBag.onePlaylist = onePlaylist;

            return View("Playlist");
        }


        [HttpPost]
        [Route("CreatePlaylist")]

        // public IActionResult CreatePlaylist (string PlaylistName, string PlaylistDescription, string PlaylistImage)
        // {
        public IActionResult CreatePlaylist ()
        {
            Console.WriteLine("------'CREATE PLAYLIST' METHOD STARTED-----");



            #region DUMMY DATA
                var playlists = new Playlist[]
                {
                    new Playlist{PlaylistName="Indie Jams", PlaylistImage="Indie Jams IMAGE", PlaylistDescription="Indie Jams DESCRIPTION" },

                    new Playlist{PlaylistName="Rap", PlaylistImage="Rap IMAGE", PlaylistDescription="Rap DESCRIPTION" },

                    new Playlist{PlaylistName="Emo", PlaylistImage="Emo IMAGE", PlaylistDescription="Emo DESCRIPTION" },
                };

                foreach(Playlist p in playlists)
                {
                    _context.Playlists.Add(p);
                }

                _context.SaveChanges();


                var tracks = new Track[]
                {
                    new Track{TrackName="Karma Police", ArtistName="Radiohead"},
                    new Track{TrackName="Let Down", ArtistName="Radiohead"},
                    new Track{TrackName="Stairway to Heaven", ArtistName="Led Zeppelin"},
                    new Track{TrackName="Black Dog", ArtistName="Led Zeppelin"},
                };

                foreach(Track t in tracks)
                    {
                    _context.Tracks.Add(t);
                }

                _context.SaveChanges();
            #endregion DUMMY DATA


            // #region OLD CODE
                // Playlist newPlaylist = new Playlist ()
                // {
                //     PlaylistName = PlaylistName,
                //     PlaylistDescription = PlaylistDescription,
                //     PlaylistImage = PlaylistImage,
                //     // Tracks = new List<Track>(),
                //     UserId = 1
                // };

                // newPlaylist.Dig();

                // newPlaylist.Intro("new playlist");
                // newPlaylist.PlaylistName.Intro("playlist name");
                // newPlaylist.PlaylistDescription.Intro("playlist description");
                // // newPlaylist.PlaylistImage.Intro("playlist image");

                // _context.Add(newPlaylist);
                // _context.SaveChanges();
            // #endregion OLD CODE

            Console.WriteLine("------'CREATE PLAYLIST' METHOD COMPLETED-----");


            return View("WorkingIndex");
        }



        [HttpPost]
        [Route("CreateNewTrack")]

        public IActionResult CreateNewTrack ()
        {
            Console.WriteLine("------'CREATE NEW TRACK' METHOD STARTED-----");


            Track newTrack = new Track ()
            {
                // PlaylistId = PlaylistId,
                TrackId = 2,
                TrackName = "Let Down",
                ArtistName = "Radiohead",
            };


            newTrack.Dig();

            // playlistTrack.Intro("playlistTrack");
            // playlistTrack.TrackName.Intro("track name");
            // playlistTrack.ArtistName.Intro("artist name");


            _context.Add(newTrack);
            _context.SaveChanges();

            Console.WriteLine("------'CREATE NEW TRACK' METHOD COMPLETED-----");


            return View("WorkingIndex");
        }



        [HttpPost]
        [Route("AddPlaylistTrack")]

        public IActionResult AddPlaylistTrack (int TrackId, int PlaylistId)

        {

            var playlistTracks = new PlaylistTrack[]
            {
                new PlaylistTrack{PlaylistId=1, TrackId=1},
                new PlaylistTrack{PlaylistId=1, TrackId=2},
                new PlaylistTrack{PlaylistId=2, TrackId=1},
                new PlaylistTrack{PlaylistId=1, TrackId=3},
            };

            foreach(PlaylistTrack pt in playlistTracks)
            {
                _context.PlaylistTracks.Add(pt);
            }

            _context.SaveChanges();

            return View("WorkingIndex");

        }


    }

}