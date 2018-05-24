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
        public IActionResult CreatePlaylist (string playlistName, string playlistDescription, string playlistImage )
        {
            Console.WriteLine("------'CREATE PLAYLIST' METHOD STARTED-----");



            #region DUMMY DATA
                var playlists = new Playlist[]
                {
                    new Playlist
                    {
                        PlaylistName = playlistName,
                        PlaylistImage = playlistDescription, PlaylistDescription=playlistImage
                    },

                };
                // var playlists = new Playlist[]
                // {
                //     new Playlist{PlaylistName="Indie Jams", PlaylistImage="Indie Jams IMAGE", PlaylistDescription="Indie Jams DESCRIPTION" },

                //     new Playlist{PlaylistName="Rap", PlaylistImage="Rap IMAGE", PlaylistDescription="Rap DESCRIPTION" },

                //     new Playlist{PlaylistName="Emo", PlaylistImage="Emo IMAGE", PlaylistDescription="Emo DESCRIPTION" },
                // };

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

                var tracks = new Track[]
                {
                    new Track{TrackName="This Is America", ArtistName="Childish Gambino"},

                    new Track{TrackName="Nice For What", ArtistName="Drake"},
                    new Track{TrackName="God's Plan", ArtistName="Drake"},
                    new Track{TrackName="Four Out of Five", ArtistName="Arctic Monkeys"},
                    new Track{TrackName="Star Treatment", ArtistName="Arctic Monkeys"},
                    new Track{TrackName="One Point Perspective", ArtistName="Arctic Monkeys"},
                    new Track{TrackName="IDGAF", ArtistName="Dua Lipa"},
                    new Track{TrackName="American Sports", ArtistName="Arctic Monkeys"},
                    new Track{TrackName="Do I Wanna Know?", ArtistName="Arctic Monkeys"},
                    new Track{TrackName="Redbone", ArtistName="Childish Gambino"},
                };

                foreach(Track t in tracks)
                    {
                    _context.Tracks.Add(t);
                }

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
                new PlaylistTrack{PlaylistId=1, TrackId=13},
                new PlaylistTrack{PlaylistId=1, TrackId=14},
                new PlaylistTrack{PlaylistId=1, TrackId=5},
                new PlaylistTrack{PlaylistId=1, TrackId=6},
                new PlaylistTrack{PlaylistId=1, TrackId=7},
                new PlaylistTrack{PlaylistId=1, TrackId=8},
                new PlaylistTrack{PlaylistId=1, TrackId=9},
                new PlaylistTrack{PlaylistId=1, TrackId=10},
                new PlaylistTrack{PlaylistId=1, TrackId=11},
                new PlaylistTrack{PlaylistId=1, TrackId=12},
            };

            // var playlistTracks = new PlaylistTrack[]
            // {
            //     new PlaylistTrack{PlaylistId=1, TrackId=1},
            //     new PlaylistTrack{PlaylistId=1, TrackId=2},
            //     new PlaylistTrack{PlaylistId=2, TrackId=1},
            //     new PlaylistTrack{PlaylistId=1, TrackId=3},
            // };

            foreach(PlaylistTrack pt in playlistTracks)
            {
                _context.PlaylistTracks.Add(pt);
            }
            // System.Console.WriteLine(playlistTrack);
            // _context.PlaylistTracks.Add(playlistTrack);
            _context.SaveChanges();

            var onePlaylist = _context.Playlists.Include(d => d.PlaylistTracks).ThenInclude(t => t.Track).SingleOrDefault(w => w.PlaylistId == 1);

            ViewBag.onePlaylist = onePlaylist;

            return View("Webplayer");

        }


    }

}