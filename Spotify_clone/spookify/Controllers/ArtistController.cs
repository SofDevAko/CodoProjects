using System;
using System.Collections.Generic;
using System.Linq;
// using System.Threading.Tasks;
// using System.Net;
// using System.Net.Http;
// using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
// using Newtonsoft.Json;
using Extensions;
using RestSharp;
using spookify.Models;


namespace spookify.Controllers
{
    public class ArtistController : Controller
    {
        private SpookifyContext _context;
        public ArtistController(SpookifyContext context)
        {
            _context = context;
        }



        // GET: /Home/
        [HttpGet]
        [Route("workingindex")]
        public IActionResult WorkingIndex()
        {



            return View();
        }



        [HttpGet]
        [Route("ArtistGetInfo")]
        public IActionResult ArtistGetInfo(string ArtistSearch)
        {
            // URL BASE ---> https://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=&api_key=278be908abb6863ead7c33ceb7899607&format=json
            string urlBase = "https://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=" + ArtistSearch + "&api_key=278be908abb6863ead7c33ceb7899607&format=json";

            // CLIENT ---> RestSharp.RestClient
            var ArtistInfoClient = new RestClient(urlBase);

            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "5352f3c0-48ca-4414-946f-5ac3f00ea9fa");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = ArtistInfoClient.Execute(request);

            var ResponseObject = response.Content;

            JObject BandNameObject= JObject.Parse(ResponseObject);

            string ArtistTags = (string)BandNameObject["artist"]["tags"]["tag"][0]["name"];
            ViewBag.ArtistTags = ArtistTags;

            string ArtistBioContent = (string)BandNameObject["artist"]["bio"]["content"];
            ViewBag.ArtistBioContent = ArtistBioContent;

            // NEW ARTIST ---> spookify.Models.Artist
            Artist newartist = new Artist()
            {
                ArtistName = (string)BandNameObject["artist"]["name"],
                ArtistMBID = (string)BandNameObject["artist"]["mbid"],
                ArtistURL = (string)BandNameObject["artist"]["url"],
                ArtistImage = (string)BandNameObject["artist"]["image"][3]["#text"],
                ArtistBio = (string)BandNameObject["artist"]["bio"]["summary"],
                ArtistListeners = (int)BandNameObject["artist"]["stats"]["listeners"],
                ArtistPlaycount = (int)BandNameObject["artist"]["stats"]["playcount"],
                Tags = new List<Tag>(),
                Albums = new List<Album>(),
            };




            // ARTIST ALBUM QUERY ---> https://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist=Radiohead&api_key=278be908abb6863ead7c33ceb7899607&format=json
            string  ArtistAlbumQuery = TopAlbumConcatenate(ArtistSearch);

            var ArtistAlbumClient= new RestClient(ArtistAlbumQuery);

            // ARTIST ALBUM REQUEST ---> RestSharp.RestRequest
            var ArtistAlbumRequest = new RestRequest(Method.GET);

            ArtistAlbumRequest.AddHeader("Postman-Token", "9ac3147e-7cb5-4905-b876-9dea9266bc29");
            ArtistAlbumRequest.AddHeader("Cache-Control", "no-cache");
            IRestResponse ArtistAlbumResponse = ArtistAlbumClient.Execute(ArtistAlbumRequest);

            var ArtistAlbumResponseObject = ArtistAlbumResponse.Content;
            JObject AlbumObject= JObject.Parse(ArtistAlbumResponseObject);
            int countA = 0;
            foreach(var Item in (string)AlbumObject["topalbums"]["album"][countA]["name"])
                {
                    Album newalbum = new Album
                    {
                        AlbumName =  (string)AlbumObject["topalbums"]["album"][countA]["name"],
                        AlbumPlaycount =(string)AlbumObject["topalbums"]["album"][countA]["playcount"],
                        AlbumImage = (string)AlbumObject["topalbums"]["album"][countA]["image"][1]["#text"],
                        AlbumURL = (string)AlbumObject["topalbums"]["album"][countA]["url"],
                    };
                    newartist.Albums.Add(newalbum);
                    countA ++;
                }


            ViewBag.Artist = newartist;
            // ArtistTopAlbum(ArtistSearch);

            return View("Artist");
        }



        public string TopAlbumConcatenate(string ArtistSearchName)
        {
            ViewBag.ArtistSearch = ArtistSearchName;
            string First = "https://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist=";
            string Band = ArtistSearchName;
            string Second = "&api_key=278be908abb6863ead7c33ceb7899607&format=json";
            string AllString = First + Band + Second;
            return AllString;
        }

    }
}