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
    public class TrackController : Controller
    {
    private SpookifyContext _context;
    public TrackController(SpookifyContext context)
    {
        _context = context;
    }
    
    
    [HttpGet]
    [Route("Track")]
    public IActionResult Track()
    {
        var client = new RestClient("https://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key=278be908abb6863ead7c33ceb7899607&format=json");
        var request = new RestRequest(Method.GET);
        request.AddHeader("Postman-Token", "81422252-3fb6-467e-af8f-0d6aba513fbd");
        request.AddHeader("Cache-Control", "no-cache");
        IRestResponse response = client.Execute(request);
        

        var allTopTracks = response.Content;                
        Console.WriteLine("#######################");
        JObject parsedTopTracks= JObject.Parse(allTopTracks);       //this parses the data from the line above
        ViewBag.allTopTracks = parsedTopTracks;

        var TopTrackNames = new List<string>();
        {
            int count = 0; 
            foreach(var item in(string)parsedTopTracks["tracks"]["track"][0]["name"])
            {
                string TrackName = (string)parsedTopTracks["tracks"]["track"][count]["name"];   
                TopTrackNames.Add((TrackName).ToString());
                count ++; 
                ViewBag.TrackNames = TopTrackNames; 
            }
            return View("Track"); 
        }
    }
    }
}