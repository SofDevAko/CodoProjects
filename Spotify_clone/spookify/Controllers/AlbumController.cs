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
            var client = new RestClient(AllString);

            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "1310a566-51fe-4f52-89c6-bbd00c851f6b");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);
            var ResponseObject = response.Content;
            JObject AlbumObject= JObject.Parse(ResponseObject);

            string AlbumName = (string)AlbumObject["album"]["name"];
            string AlbumArtist = (string)AlbumObject["album"]["artist"];
            string AlbumMBID = (string)AlbumObject["album"]["mbid"];
            string AlbumURL = (string)AlbumObject["album"]["url"];
            string AlbumListeners = (string)AlbumObject["album"]["listeners"];
            string AlbumPlaycount = (string)AlbumObject["album"]["playcount"]; 
            // string AllAlbumInfo = String.Join(", ", "Album Name:", AlbumName, "Artist:", AlbumArtist, "MBID:",  AlbumMBID, "URL:", AlbumURL, "Listeners:", AlbumListeners, "Playcount:", AlbumPlaycount); 
            // Console.WriteLine(AllAlbumInfo); 

            ViewBag.AlbumName = AlbumName;
            ViewBag.AlbumListeners = AlbumListeners; 
            ViewBag.AlbumPlaycount = AlbumPlaycount; 
            ViewBag.AlbumArtist = AlbumArtist; 
            // ViewBag.AlbumInfo = AllAlbumInfo; 
            
            Album newalbum = new Album(){
                AlbumName = AlbumName,
                AlbumArtist = AlbumArtist,
                AlbumMBID = AlbumMBID,
                AlbumURL = AlbumURL,
                AlbumListeners = AlbumListeners,
                AlbumPlaycount = AlbumPlaycount,
                AlbumTracks = new List<Track>()
            };

            
            var TrackNames = new List<string>();
            var TrackURLs = new List<string>();
            var TrackRanks = new List<string>(); 
            var TrackDurations = new List<string>(); 
            var AllAlbumItems = new List<string>(); 
            var SingleAlbumItems = new List<string>(); 
            
            {
                int countUp = 0; 
                int CountH = ((JArray)AlbumObject["album"]["tracks"]["track"]).Count(); 
                ViewBag.Count = CountH; 
                foreach(var Item in (string)AlbumObject["album"]["tracks"]["track"][countUp]["name"])
                {
                    Track newtrack = new Track
                    {
                        TrackRank = (int)AlbumObject["album"]["tracks"]["track"][countUp]["@attr"]["rank"],
                        TrackName = (string)AlbumObject["album"]["tracks"]["track"][countUp]["name"],
                        TrackURL = (string)AlbumObject["album"]["tracks"]["track"][countUp]["url"],
                        TrackDuration = (int)AlbumObject["album"]["tracks"]["track"][countUp]["duration"],
                        
                    };
                    System.Console.WriteLine(newalbum.AlbumTracks);
                    System.Console.WriteLine("=============================================");
                    System.Console.WriteLine(newtrack);
                    newalbum.AlbumTracks.Add(newtrack);
                    
                    string TrackRank = (string)AlbumObject["album"]["tracks"]["track"][countUp]["@attr"]["rank"];
                    TrackRanks.Add((TrackRank).ToString());
                    AllAlbumItems.Add((TrackRank).ToString()); 
                    ViewBag.TrackRanks = TrackRanks;
                    
                    string TrackName = (string)AlbumObject["album"]["tracks"]["track"][countUp]["name"]; 
                    System.Console.WriteLine(TrackName);
                    TrackNames.Add((TrackName).ToString());
                    AllAlbumItems.Add((TrackName).ToString());  
                    ViewBag.TrackNames = TrackNames; 
                    
                    string TrackURL = (string)AlbumObject["album"]["tracks"]["track"][countUp]["url"]; 
                    TrackURLs.Add((TrackURL).ToString()); 
                    AllAlbumItems.Add(TrackURL); 
                    ViewBag.TrackURLs = TrackURLs; 
                    // Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$"); 
                    // Console.WriteLine(AllItemInfos); 
                    
                    string TrackDuration = (string)AlbumObject["album"]["tracks"]["track"][countUp]["duration"]; 
                    TrackDurations.Add((TrackDuration).ToString()); 
                    AllAlbumItems.Add((TrackDuration).ToString()); 
                    ViewBag.TrackDurations = TrackDurations; 


                    
                    countUp++; 
                    
                    SingleAlbumItems.Add((AllAlbumItems).ToString()); 
                    // Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    // Console.WriteLine(SingleAlbumItems);                                //System.Collections.Generic.List`1[System.String]
                    ViewBag.SingleAlbumItems = SingleAlbumItems; 
                    // Console.WriteLine(AllSingleInfo.GetType());                     //System.Collections.Generic.List`1[System.String]
                    // Console.WriteLine(AllItemInfo.GetType());                       //System.Collections.Generic.List`1[System.String]
                    // Console.WriteLine(TrackRank.GetType());                         //System.String
                    
                }
                Console.WriteLine(AllAlbumItems[0]); 
                // AllSingleInfo.ForEach(x => x.ForEach(Console.WriteLine); 
                ViewBag.AllAlbumItems= AllAlbumItems; 
                ViewBag.album = newalbum;
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
                    // ViewBag.TagName = TagNames;
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