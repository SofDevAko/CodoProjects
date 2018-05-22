using System;
// using System.Collections.Generic;
using System.Linq;
// using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Http;
// using Newtonsoft.Json.Linq;
// using Newtonsoft.Json;
using spookify.Models;
// using RestSharp;

namespace spookify.Controllers
{
    [Route("api/ArtistController")]
    public class ApiController : Controller
    {
        private readonly SpookifyContext _context;

        public ApiController(SpookifyContext context)
        {
            _context = context;

            if (_context.Artists.Count() == 0)
            {
                _context.Artists.Add(new Artist { ArtistName = "Artist1" });
                _context.SaveChanges();
            }
        }
    }
}