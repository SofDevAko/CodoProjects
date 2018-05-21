using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Extensions;
using RestSharp;
using spookify.Models;

namespace spookify.Controllers
{
    public class SearchController : Controller
    {
        private SpookifyContext _context;

        public SearchController(SpookifyContext context)
        {
            _context = context;
        }


        // string APIkey = "278be908abb6863ead7c33ceb7899607";



        // view search results for everything
        [HttpGet]
        [Route("FullSearch")]
        public IActionResult FullSearch (string SearchQuery)
        {
            Console.WriteLine("------'VIEW FULL SEARCH' METHOD STARTED-----");

            GetAlbumSearchResults(SearchQuery);
            GetArtistSearchResults(SearchQuery);
            GetTrackSearchResults(SearchQuery);

            Console.WriteLine("------'VIEW FULL SEARCH' METHOD COMPLETED-----");

            return View("SearchResults");
        }


        // get artist search results
        [HttpGet]
        [Route("GetArtistSearchResults")]
        public JsonResult GetArtistSearchResults(string SearchQuery)
        {
            string urlBase = "http://ws.audioscrobbler.com/2.0/?method=artist.search&artist=" + SearchQuery + "&api_key=278be908abb6863ead7c33ceb7899607&format=json";

            var client = new RestClient(urlBase);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "b66a653e-ca6a-4763-b3fd-1166a22b2c38");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);

            var artistSearchResults= response.Content;
            JObject parsedSearchResults= JObject.Parse(artistSearchResults);

            var ArtistsList = new List<Artist>();

            foreach(var item in parsedSearchResults["results"]["artistmatches"]["artist"])
            {
                Artist Artist = new Artist ()
                {
                    ArtistName = (string)item["name"],
                    ArtistMBID = (string)item["mbid"],
                    ArtistImage = (string)item["image"][1]["#text"],
                    ArtistListeners = (int)item["listeners"],
                    ArtistURL = (string)item["url"]
                };
                ArtistsList.Add(Artist);
                ArtistsList.Intro("artists list");
            }

            ViewBag.ArtistsList = ArtistsList;

            return Json(ArtistsList);
        }


        // get album search results
        [HttpGet]
        [Route("GetAlbumSearchResults")]
        public JsonResult GetAlbumSearchResults(string SearchQuery)
        {
            string urlBase = "http://ws.audioscrobbler.com/2.0/?method=album.search&album=" + SearchQuery + "&api_key=278be908abb6863ead7c33ceb7899607&format=json";

            var client = new RestClient(urlBase);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "956c53ec-c978-421f-87a2-ae981998ebaa");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);

            var albumSearchResults = response.Content;
            JObject parsedSearchResults= JObject.Parse(albumSearchResults);

            var AlbumsList = new List<Album>();

            foreach(var item in parsedSearchResults["results"]["albummatches"]["album"])
            {
                Album Album = new Album ()
                {
                    AlbumName = (string)item["name"],
                    AlbumArtist = (string)item["artist"],
                    AlbumMBID = (string)item["mbid"],
                    AlbumImage = (string)item["image"][1]["#text"],
                    AlbumURL = (string)item["url"]
                };
                AlbumsList.Add(Album);
                AlbumsList.Intro("albums list");
            }

            ViewBag.AlbumsList = AlbumsList;

            return Json(AlbumsList);
        }


        // get Track search results
        [HttpGet]
        [Route("GetTrackSearchResults")]
        public JsonResult GetTrackSearchResults(string SearchQuery)
        {
            string urlBase = "http://ws.audioscrobbler.com/2.0/?method=track.search&track=" + SearchQuery + "&api_key=278be908abb6863ead7c33ceb7899607&format=json";

            var client = new RestClient(urlBase);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "17f6e0a0-2f75-4849-9354-4c13c094e903");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);

            var trackSearchResults = response.Content;
            JObject parsedSearchResults= JObject.Parse(trackSearchResults);

            var TracksList = new List<Track>();

            foreach(var item in parsedSearchResults["results"]["trackmatches"]["track"])
            {
                Track Track = new Track ()
                {
                    TrackName = (string)item["name"],
                    TrackMBID = (string)item["mbid"],
                    ArtistName = (string)item["artist"],
                    TrackImage = (string)item["image"][1]["#text"],
                    TrackURL = (string)item["url"],
                    TrackListeners = (int)item["listeners"]
                };
                TracksList.Add(Track);
                TracksList.Intro("tracks list");
            }

            ViewBag.TracksList = TracksList;

            return Json(TracksList);
        }


    }
}