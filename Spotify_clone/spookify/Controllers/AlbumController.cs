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
using RestSharp;
using spookify.Models;

namespace spookify.Controllers
{
    public class AlbumController : Controller
    {
    private SpookifyContext _context;
    public AlbumController(SpookifyContext context)
    {
        _context = context;
    }



    [HttpGet]
    [Route("SingleAlbumInfo")]
        public IActionResult SingleAlbumInfo(string albumsearch, string albumartistsearch)
        {
            string AllString = AlbumConcatenate(albumsearch, albumartistsearch);
            var SingleAlbumInfoClient = new RestClient(AllString);

            var SingleAlbumInfoRequest = new RestRequest(Method.GET);
            SingleAlbumInfoRequest.AddHeader("Postman-Token", "1310a566-51fe-4f52-89c6-bbd00c851f6b");
            SingleAlbumInfoRequest.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = SingleAlbumInfoClient.Execute(SingleAlbumInfoRequest);

            var SingleAlbumResponseObject = response.Content;
            JObject AlbumObject= JObject.Parse(SingleAlbumResponseObject);

            Album SingleAlbum = new Album(){
                AlbumName = (string)AlbumObject["album"]["name"],
                AlbumArtist = (string)AlbumObject["album"]["artist"],
                AlbumMBID = (string)AlbumObject["album"]["mbid"],
                AlbumURL = (string)AlbumObject["album"]["url"],
                AlbumListeners = (string)AlbumObject["album"]["listeners"],
                AlbumPlaycount = (string)AlbumObject["album"]["playcount"],
                AlbumTracks = new List<Track>()
            };

            ViewBag.SingleAlbum = SingleAlbum;

            var TrackNames = new List<string>();
            var TrackURLs = new List<string>();
            var TrackRanks = new List<string>();
            var TrackDurations = new List<string>();

            {
                int countUp = 0;
                foreach(var Item in (string)AlbumObject["album"]["tracks"]["track"][countUp]["name"])
                {
                    Track newtrack = new Track
                    {
                        TrackRank = (int)AlbumObject["album"]["tracks"]["track"][countUp]["@attr"]["rank"],
                        TrackName = (string)AlbumObject["album"]["tracks"]["track"][countUp]["name"],
                        TrackURL = (string)AlbumObject["album"]["tracks"]["track"][countUp]["url"],
                        TrackDuration = (int)AlbumObject["album"]["tracks"]["track"][countUp]["duration"],
                    };

                    SingleAlbum.AlbumTracks.Add(newtrack);

                    string TrackRank = (string)AlbumObject["album"]["tracks"]["track"][countUp]["@attr"]["rank"];
                        TrackRanks.Add((TrackRank).ToString());

                    string TrackName = (string)AlbumObject["album"]["tracks"]["track"][countUp]["name"];
                        TrackNames.Add((TrackName).ToString());

                    string TrackURL = (string)AlbumObject["album"]["tracks"]["track"][countUp]["url"];
                        TrackURLs.Add((TrackURL).ToString());

                    string TrackDuration = (string)AlbumObject["album"]["tracks"]["track"][countUp]["duration"];
                        TrackDurations.Add((TrackDuration).ToString());

                    countUp++;

                }

                ViewBag.album = SingleAlbum;
            }

            //ALBUM IMAGES
            var ImageURLs = new List<string>();
            var ImageSize = new List<string>();
            {
                int count = 0;
                foreach (var URL in (string)AlbumObject["album"]["image"][count]["size"])
                {
                    string ImageSizeString = (string)AlbumObject["album"]["image"][count]["size"];
                        ImageSize.Add((ImageSizeString).ToString());

                    string ImageURLstring = (string)AlbumObject["album"]["image"][count]["#text"];
                        ImageURLs.Add((ImageURLstring).ToString());

                    count ++;
                }
                ViewBag.MedImage = (string)AlbumObject["album"]["image"][2]["#text"];
            }

            // ALBUM TAGS
            var TagNames = new List<string>();
            var TagURLs = new List<string>();
            {
                int countTags = 0;
                // Console.WriteLine(countA);
                foreach(var TagItem in (string)AlbumObject["album"]["tags"]["tag"][0]["url"])
                {
                    string TagURL = (string)AlbumObject["album"]["tags"]["tag"][0]["url"];
                        TagURLs.Add((TagURL).ToString());

                    countTags ++;
                    ViewBag.TagURLs = TagURLs;
                }
            }

            return View("SingleAlbum");
        }


        public string AlbumConcatenate(string albumsearch, string albumartistsearch)
        {
            ViewBag.albumsearch = albumsearch;
            ViewBag.albumartistsearch = albumartistsearch;
            string First = "https://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=278be908abb6863ead7c33ceb7899607&artist=";
            string AlbumArtist = albumartistsearch;
            string Second = "&album=";
            string Album = albumsearch;
            string Third = "&format=json";
            string AllString = First + AlbumArtist + Second + Album +  Third;
            return AllString;
         }
    }
}