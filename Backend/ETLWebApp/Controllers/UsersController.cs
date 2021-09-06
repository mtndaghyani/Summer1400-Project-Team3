using System.Collections.Generic;
using System.Linq;
using ETLLibrary.Authentication;
using ETLLibrary.Database;
using ETLLibrary.Database.Models;
using ETLLibrary.Interfaces;
using ETLWebApp.Models.AuthenticationModels;
using Microsoft.AspNetCore.Mvc;

namespace ETLWebApp.Controllers
{
    [ApiController]
    [Route("users/")]
    public class UsersController : Controller
    {
        private EtlContext _context;
        private IAuthenticator _authenticator;
        private ICsvDatasetManager _csvManager;
        private ISqlServerDatasetManager _sqlServerManager;

        public UsersController(EtlContext context, IAuthenticator authenticator, ICsvDatasetManager csvManager,
            ISqlServerDatasetManager sqlServerManager)
        {
            _context = context;
            _authenticator = authenticator;
            _csvManager = csvManager;
            _sqlServerManager = sqlServerManager;
        }

        [HttpPost("signup")]
        public ActionResult SignUp(RegisterModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(new {Message = "User registered successfully!"});
        }

        [HttpPost("login")]
        public ActionResult Login(LoginModel model)
        {
            var user = _authenticator.ValidateUser(model.Username, model.Password);
            if (user != null)
            {
                var token = _authenticator.Login(user);
                return Ok(new {Token = token});
            }

            return Unauthorized("Authentication failed");
        }

        [HttpPost("logout")]
        public ActionResult Logout(LogoutModel model)
        {
            _authenticator.Logout(model.Token);
            return Ok(new {Message = "User logout successfully!"});
        }

        [HttpGet("{username}/csvs")]
        public ActionResult GetCsvFiles(string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound(new {Message = "User with this username not found"});
            }

            return Ok(new {CsvFiles = _csvManager.GetCsvFiles(username)});
        }

        [HttpGet("{username}/sqlservers")]
        public ActionResult GetSqlServerDbNames(string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
            {
                return NotFound(new {Message = "User with this username not found"});
            }

            return Ok(new {DbNames = _sqlServerManager.GetDatasets(username)});
        }


        [HttpGet("{token}")]
        public ActionResult GetUserByToken(string token)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            return Ok(user);
        }
    }
}