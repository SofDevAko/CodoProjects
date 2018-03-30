using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
// my using statements
using System.Linq;
using spookify.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using static spookify.Models.RegUser;
using static spookify.Models.LogReg;

namespace spookify.Controllers
{
    public class UserController : Controller
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

        public UserController(SpookifyContext context)
        {
            // Dapper framework connections
            // _dbConnector = connect;
            // userFactory = new UserFactory();

            // Entity Framework connections
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [Route("register")]
        public IActionResult RegisterPage()
        {
            return View("Register");
        }

        [HttpPost]
        [Route("register/user")]
        public IActionResult CreateUser(string Email, string ConfirmEmail, string Password, string Username, DateTime DoB, string Gender)
        {
            RegUser reguser = new RegUser{
                Email = Email,
                ConfirmEmail = ConfirmEmail,
                Password = Password,
                Username = Username,
                DoB = DoB,
                Gender = Gender,
            };
            var res = TryValidateModel(reguser);
            System.Console.WriteLine(res);
            User test = _context.Users.SingleOrDefault(u => u.Email == reguser.Email);
            User test2 = _context.Users.SingleOrDefault(u => u.Username == reguser.Username);
            if(test != null)
            {
                ModelState.AddModelError(string.Empty,"This email is already registered!");
            }
            if(test2 != null)
            {
                ModelState.AddModelError(string.Empty,"This Username is registered. Please try another Username!");
            }
            if (ModelState.IsValid)
            {
                User newuser = new User()
                {
                    Username = reguser.Username,
                    Email = reguser.Email,
                    DoB = reguser.DoB,
                    Password = reguser.Password,
                    Gender = reguser.Gender,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Add(newuser);
                _context.SaveChanges();
                User user = _context.Users.SingleOrDefault(u => u.Email == reguser.Email);
                HttpContext.Session.SetInt32("ActiveId", user.UserId);
                return RedirectToAction("Webplayer", "Webplayer");
            }
            
            return View("Register");
        }
        [HttpGet]
        [Route("login")]
        public IActionResult LoginPage()
        {
            return View("Login");
        }
        [HttpPost]
        [Route("login/user")]
        public IActionResult Login(string Email, string Password)
        {
            LoginUser loguser = new LoginUser{
                Email = Email,
                Password = Password,
            };
            TryValidateModel(loguser);
            User curuser = _context.Users.SingleOrDefault(u => u.Email == loguser.Email);
            if (curuser != null)
            {
                if (ModelState.IsValid)
                {
                    int ActiveId = curuser.UserId;
                    string ActiveUserName = curuser.Username;
                    HttpContext.Session.SetInt32("ActiveId", ActiveId);
                    return RedirectToAction("Webplayer","Webplayer");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong password!");
                    return View("Login");
                }
                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email not registered!");
                return View("Login");
            }
            
            
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

    }
}
