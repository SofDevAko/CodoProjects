using System;
using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Net;
// using System.Net.Http;
// using System.Web;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
// using Newtonsoft.Json;
using Extensions;
using RestSharp;
using spookify.Models;

namespace spookify.Controllers
{
    public class ChartController : Controller
    {
        private SpookifyContext _context;

        public ChartController(SpookifyContext context)
        {
            _context = context;
        }


        #region GLOBAL ARTISTS, TRACKS, TAGS

            // view charts for artists, tracks, tags
            [HttpGet]
            [Route("ViewGlobalCharts")]
            public IActionResult ViewGlobalCharts ()
            {
                Console.WriteLine("------'VIEW TOP CHART' METHOD STARTED-----");

                GetGlobalArtists();
                GetGlobalTracks();
                GetGlobalTags();

                Console.WriteLine("------'VIEW TOP CHART' METHOD COMPLETED-----");

                return View("Chart.Global");
            }


            // get top global artist data
            [HttpGet]
            [Route("GetGlobalArtists")]
            public JsonResult GetGlobalArtists()
            {
                var client = new RestClient("https://ws.audioscrobbler.com/2.0/?method=chart.gettopartists&api_key=278be908abb6863ead7c33ceb7899607&format=json");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Postman-Token", "d9f0274a-7b00-499a-8924-a1cf9da041c7");
                request.AddHeader("Cache-Control", "no-cache");
                IRestResponse response = client.Execute(request);

                var allTopArtists = response.Content;
                JObject parsedTopArtists= JObject.Parse(allTopArtists);

                var GlobalArtists = new List<Artist>();

                foreach(var item in parsedTopArtists["artists"]["artist"])
                {
                    Artist GlobalArtist = new Artist ()
                    {
                        ArtistName = (string)item["name"],
                        ArtistMBID = (string)item["mbid"],
                        ArtistImage = (string)item["image"][1]["#text"],
                        ArtistPlaycount = (int?)item["playcount"],
                        ArtistListeners = (int)item["listeners"],
                        ArtistURL = (string)item["url"]
                    };
                    GlobalArtists.Add(GlobalArtist);
                }

                ViewBag.GlobalArtists = GlobalArtists;

                return Json(GlobalArtists);
            }


            // get global top tracks
            [HttpGet]
            [Route("GetGlobalTracks")]
            public JsonResult GetGlobalTracks()
            {
                var client = new RestClient("https://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key=278be908abb6863ead7c33ceb7899607&format=json");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Postman-Token", "81422252-3fb6-467e-af8f-0d6aba513fbd");
                request.AddHeader("Cache-Control", "no-cache");

                IRestResponse response = client.Execute(request);


                var allTopTracks = response.Content;

                JObject parsedTopTracks= JObject.Parse(allTopTracks);

                ViewBag.allTopTracks = parsedTopTracks;

                var GlobalTracks = new List<Track>();

                foreach(var item in parsedTopTracks["tracks"]["track"])
                {
                    Track GlobalTrack = new Track ()
                    {
                        TrackName = (string)item["name"],
                        TrackDuration = (int)item["duration"],
                        TrackPlaycount = (int)item["playcount"],
                        TrackListeners = (int)item["listeners"],
                        TrackURL = (string)item["url"],
                        TrackMBID = (string)item["mbid"],
                        TrackImage = (string)item["image"][1]["#text"],
                        ArtistName = (string)item["artist"]["name"],
                        ArtistMBID = (string)item["artist"]["mbid"],
                        ArtistURL = (string)item["artist"]["url"]
                    };
                    GlobalTracks.Add(GlobalTrack);

                }

                ViewBag.GlobalTracks = GlobalTracks;

                return Json(GlobalTracks);

            }


            // get global top tags
            [HttpGet]
            [Route("GetGlobalTags")]
            public JsonResult GetGlobalTags()
            {
                // #region POSTMAN
                    var client = new RestClient("https://ws.audioscrobbler.com/2.0/?method=chart.gettopTags&api_key=278be908abb6863ead7c33ceb7899607&format=json");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Postman-Token", "81422252-3fb6-467e-af8f-0d6aba513fbd");
                    request.AddHeader("Cache-Control", "no-cache");
                    IRestResponse response = client.Execute(request);
                // #endregion POSTMAN

                var allTopTags = response.Content;

                JObject parsedTopTags= JObject.Parse(allTopTags);

                var GlobalTags = new List<Tag>();

                foreach(var item in parsedTopTags["tags"]["tag"])
                {
                    Tag GlobalTag = new Tag ()
                    {
                        TagName = (string)item["name"],
                        TagURL = (string)item["url"],
                        TagReach = (int)item["reach"],
                        TagTaggings = (int)item["taggings"],
                    };

                    GlobalTags.Add(GlobalTag);

                }

                ViewBag.GlobalTags = GlobalTags;

                return Json(GlobalTags);

            }

        #endregion



        #region GEOGRAPHY ARTISTS AND TRACKS

            // view top artists and tracks for a specific country/geo
            [HttpGet]
            [Route("ViewGeoCharts")]
            public IActionResult ViewGeoCharts (string GeoSearch)
            {
                Console.WriteLine("------'VIEW GEO TOP CHART' METHOD STARTED-----");

                ViewBag.Geo = GeoSearch;
                GetGeoArtists(GeoSearch);
                GetGeoTracks(GeoSearch);

                Console.WriteLine("------'VIEW GEO TOP CHART' METHOD COMPLETED-----");

                return View("Chart.Geo");
            }


            // get top artists for a specific country/geo
            [HttpGet]
            [Route("GetGeoArtists")]
            public JsonResult GetGeoArtists(string GeoSearch)
            {

                Console.WriteLine("------'VIEW GEO ARTIST' METHOD STARTED-----");

                string urlBase = "http://ws.audioscrobbler.com/2.0/?method=geo.gettopartists&country=" + GeoSearch + "&api_key=278be908abb6863ead7c33ceb7899607&format=json";

                // #region POSTMAN
                    // CLIENT ---> 'RestSharp.RestClient'
                    var client = new RestClient(urlBase);

                    // REQUEST ---> 'RestSharp.RestRequest'
                    var request = new RestRequest(Method.GET);

                    request.AddHeader("Postman-Token", "4127af80-0e24-4180-83ae-e3be93581df9");
                    request.AddHeader("Cache-Control", "no-cache");

                    // RESPONSE ---> 'RestSharp.RestResponse'
                    IRestResponse response = client.Execute(request);
                // #endregion POSTMAN

                // ALL TOP ARTISTS ---> un-parsed JSON
                var allTopArtists = response.Content;

                // PARSED TOP ARTISTS ---> parsed JSON
                JObject parsedTopArtists = JObject.Parse(allTopArtists);
                    // parsedTopArtists.Intro("PARSED TOP ARTISTS");

                var GeoArtists = new List<Artist>();

                foreach(var item in parsedTopArtists["topartists"]["artist"])
                {
                    Artist geoArtist = new Artist ()
                    {
                        ArtistName = (string)item["name"],
                        ArtistMBID = (string)item["mbid"],
                        ArtistImage = (string)item["image"][1]["#text"],
                        ArtistPlaycount = (int?)item["playcount"],
                        ArtistListeners = (int)item["listeners"],
                        ArtistURL = (string)item["url"]
                    };
                    GeoArtists.Add(geoArtist);
                }

                ViewBag.GeoArtists = GeoArtists;

                return Json(GeoArtists);
            }


            // get top tracks for a specific country/geo
            [HttpGet]
            [Route("GetGeoTracks")]
            public JsonResult GetGeoTracks(string GeoSearch)
            {

                Console.WriteLine("------'VIEW GEO TRACKS' METHOD STARTED-----");

                string urlBase = "http://ws.audioscrobbler.com/2.0/?method=geo.gettoptracks&country=" + GeoSearch + "&api_key=278be908abb6863ead7c33ceb7899607&format=json";
                    urlBase.Intro("URL BASE");

                // #region POSTMAN
                    // CLIENT ---> 'RestSharp.RestClient'
                    var client = new RestClient(urlBase);

                    // REQUEST ---> 'RestSharp.RestRequest'
                    var request = new RestRequest(Method.GET);

                    request.AddHeader("Postman-Token", "3c6c1277-d525-48e9-a1c5-ba82e7bcd5cc");
                    request.AddHeader("Cache-Control", "no-cache");

                    // RESPONSE ---> 'RestSharp.RestResponse'
                    IRestResponse response = client.Execute(request);
                // #endregion POSTMAN

                var geoTopTracks = response.Content;
                    // geoTopTracks.Intro("ALL TOP TRACKS");

                JObject parsedGeoTracks= JObject.Parse(geoTopTracks);
                    // parsedTopTracks.Intro("parsed top tracks");

                ViewBag.geoTopTracks = parsedGeoTracks;

                var GeoTracks = new List<Track>();

                foreach(var item in parsedGeoTracks["tracks"]["track"])
                {
                    item.Intro("ITEM");
                    Track geoTrack = new Track ()
                    {
                        TrackName = (string)item["name"],
                        TrackDuration = (int)item["duration"],
                        TrackListeners = (int)item["listeners"],
                        TrackURL = (string)item["url"],
                        TrackMBID = (string)item["mbid"],
                        TrackImage = (string)item["image"][1]["#text"],
                        ArtistName = (string)item["artist"]["name"],
                        ArtistMBID = (string)item["artist"]["mbid"],
                        ArtistURL = (string)item["artist"]["url"]
                    };
                    GeoTracks.Add(geoTrack);
                }

                GeoTracks.Intro("GET TRACKS");

                ViewBag.GeoTracks = GeoTracks;

                return Json(GeoTracks);
            }

        #endregion GEOGRAPHY ARTISTS AND TRACKS
    }
}