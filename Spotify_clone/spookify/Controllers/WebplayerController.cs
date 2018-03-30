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
            int? firstid = HttpContext.Session.GetInt32("ActiveId");
            
            if(firstid != null)
            {
                int id = (int)firstid;
                User curuser = _context.Users.SingleOrDefault(u=>u.UserId == id);
                ViewBag.User = curuser;
                return View("Webplayer");
            }
            else
            {
                return View("Index");
            }
            
        }
    }
}