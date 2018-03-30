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
    public class WebplayerController : Controller
    {
        private SpookifyContext _context;
        public WebplayerController(SpookifyContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Webplayer")]
        
        public IActionResult Webplayer()
        {
            return View(); 
        }
    }
}