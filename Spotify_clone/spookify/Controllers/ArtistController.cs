using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using spookify.Models; 
using RestSharp; 

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
    [Route("")]
    public IActionResult Index()
    {
        var client = new RestClient("https://ws.audioscrobbler.com/2.0/?method=chart.gettopartists&api_key=278be908abb6863ead7c33ceb7899607&format=json&page=1");
        var request = new RestRequest(Method.GET);
        request.AddHeader("Postman-Token", "8f925137-8a21-4dd5-9e77-46c0eba86a04");
        request.AddHeader("Cache-Control", "no-cache");
        IRestResponse response = client.Execute(request);

        var allTopArtistData = response.Content;                //this gets you all the data in Last.FMs Top Artist query
        JObject parsedTopArtistData= JObject.Parse(allTopArtistData);       //this parses the data from the line above
        ViewBag.allTopArtistData = parsedTopArtistData;

        string firstArtistName = (string)parsedTopArtistData["artists"]["artist"][0]["name"];               //return name of first artist in the artists data (Kendrick Lamar)
        ViewBag.firstArtistName = firstArtistName;              //sends Kendrick Lamar to the ViewBag

        string listeners = (string)parsedTopArtistData["artists"]["artist"][0]["listeners"];                //this gives you Kendrick Lamar's number of listeners
        ViewBag.listeners = listeners;              //this sends Kendrick's # of listeners to the ViewBag

        string playcount = (string)parsedTopArtistData["artists"]["artist"][0]["playcount"];                //this gives you Kendrick Lamar's number of listeners
        ViewBag.playcount = playcount;

        string mbid = (string)parsedTopArtistData["artists"]["artist"][0]["mbid"];
        ViewBag.mbid = mbid;

        string URL = (string)parsedTopArtistData["artists"]["artist"][0]["url"];
        ViewBag.URL = URL;

        return View(); 
    }

    [HttpGet]
    [Route("ArtistGetInfo")]
    public IActionResult ArtistGetInfo(string ArtistSearch)
    {
        ViewBag.ArtistSearch = ArtistSearch; 
        string First = "https://ws.audioscrobbler.com/2.0/?";
        string Second = "method=artist.getinfo&";
        string Third = "artist=";
        string Band = ArtistSearch; 
        string Fourth = "&api_key=278be908abb6863ead7c33ceb7899607";
        string Fifth = "&format=json";
        string AllString = First + Second + Third + Band + Fourth + Fifth;
        System.Console.WriteLine(AllString); 
        var client = new RestClient(AllString);
        
        var request = new RestRequest(Method.GET);
        request.AddHeader("Postman-Token", "5352f3c0-48ca-4414-946f-5ac3f00ea9fa");
        request.AddHeader("Cache-Control", "no-cache");
        IRestResponse response = client.Execute(request);

        var ResponseObject = response.Content;
        Console.WriteLine(ResponseObject);
        JObject BandNameObject= JObject.Parse(ResponseObject);

        string ArtistName = (string)BandNameObject["artist"]["name"];
        ViewBag.ArtistName = ArtistName;

        string ArtistMBID = (string)BandNameObject["artist"]["mbid"];
        ViewBag.ArtistMBID = ArtistMBID;

        string ArtistListeners = (string)BandNameObject["artist"]["stats"]["listeners"];
        ViewBag.ArtistListeners = ArtistListeners;

        string ArtistPlaycount = (string)BandNameObject["artist"]["stats"]["playcount"];
        ViewBag.ArtistPlaycount = ArtistPlaycount;

        string ArtistURL = (string)BandNameObject["artist"]["url"];
        ViewBag.ArtistURL = ArtistURL;

        string ArtistTags = (string)BandNameObject["artist"]["tags"]["tag"][0]["name"]; 
        ViewBag.ArtistTags = ArtistTags; 

        string ArtistBIO = (string)BandNameObject["artist"]["bio"]["summary"];
        ViewBag.ArtistBIO = ArtistBIO; 

        string ArtistBioContent = (string)BandNameObject["artist"]["bio"]["content"];
        ViewBag.ArtistBioContent = ArtistBioContent; 

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
        
        
        
        
        [HttpGet]
        [Route("ArtistTopAlbum")]

        public IActionResult ArtistTopAlbum(string ArtistSearchName)
        {
            string  AllString = TopAlbumConcatenate(ArtistSearchName); 
            var client = new RestClient(AllString);

            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "9ac3147e-7cb5-4905-b876-9dea9266bc29");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);

            var ResponseObject = response.Content;
            JObject AlbumObject= JObject.Parse(ResponseObject);


            for (var i = 0; i < AlbumObject.Count; i++)
            {
                string AlbumName = (string)AlbumObject["topalbums"]["album"][i]["name"];
                Console.WriteLine(AlbumName);

                string AlbumPlaycount = (string)AlbumObject["topalbums"]["album"][i]["playcount"];
                Console.WriteLine(AlbumPlaycount);

                string AlbumMBID = (string)AlbumObject["topalbums"]["album"][i]["MBID"];
                Console.WriteLine(AlbumMBID);
            }

            string FirstAlbumName = (string)AlbumObject["topalbums"]["album"][0]["name"];
            ViewBag.AlbumName = FirstAlbumName;
            ViewBag.AlbumObject = AlbumObject; 
            return View("Album");
        }
    }
}







