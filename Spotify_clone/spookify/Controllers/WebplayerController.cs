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
// my using statements
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using static spookify.Models.RegUser;
using static spookify.Models.LogReg;

namespace spookify.Controllers
{
    public class WebplayerController : Controller
    {
        // ########## ROUTES ##########
        //  /
        //  /(add_routes_guide)
        //  /
        // ########## ROUTES ##########

        // Dapper connections
        // private readonly UserFactory userFactory;
        // private readonly DbConnector _dbConnector;

        // Entity PostGres Code First connection
        private SpookifyContext _context;

        public WebplayerController(SpookifyContext context)
        {
            // Dapper framework connections
            // _dbConnector = connect;
            // userFactory = new UserFactory();

            // Entity Framework connections
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("webplayer")]
        public IActionResult Webplayer()
        {
            return View("Webplayer");
        }
        
    [HttpGet]
    [Route("worker")]
    public IActionResult WorkingIndex()
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
        ViewBag.listeners = listeners;              //this sends Kendrick's # of listeners to the 
        
        // string image = (string)parsedTopArtistData["artists"]["artist"]["image"][0]["#text"];
        // ViewBag.image = image; 

        string playcount = (string)parsedTopArtistData["artists"]["artist"][0]["playcount"];                //this gives you Kendrick Lamar's number of listeners
        ViewBag.playcount = playcount;

        string mbid = (string)parsedTopArtistData["artists"]["artist"][0]["mbid"];
        ViewBag.mbid = mbid;

        string URL = (string)parsedTopArtistData["artists"]["artist"][0]["url"];
        ViewBag.URL = URL;
        
        var TopArtist = new List<string>(); 
        {
            int CountA = 0;
            foreach(var item in (string)parsedTopArtistData["artists"]["artist"][CountA]["name"])
            {
                Artist topartist = new Artist
                {   
                    ArtistName = firstArtistName,
                    ArtistURL = URL,
                    ArtistListeners2 = listeners,
                    ArtistPlaycount2 = playcount,
                 };
                TopArtist.Add((topartist).ToString()); 
                Console.WriteLine(TopArtist);
                CountA++; 
            }
            ViewBag.top = TopArtist; 
        }
        return View("Webplayer");
    }
    }
}