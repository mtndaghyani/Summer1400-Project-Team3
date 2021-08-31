using System.Collections.Generic;
using ETLLibrary.Authentication;
using ETLLibrary.Database;
using ETLLibrary.Interfaces;
using ETLWebApp.Models.AuthenticationModels;
using Microsoft.AspNetCore.Mvc;

namespace ETLWebApp.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private EtlContext _context;
        private IAuthenticator _authenticator;
        private ICsvDatasetManager _manager;

        public UsersController(EtlContext context, IAuthenticator authenticator, ICsvDatasetManager manager)
        {
            _context = context;
            _authenticator = authenticator;
            _manager = manager;
        }

        [HttpPost("/signup")]
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

        [HttpPost("/login")]
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

        [HttpPost("/logout")]
        public ActionResult Logout(LogoutModel  model)
        {
            _authenticator.Logout(model.Token);
            return Ok(new {Message = "User logout successfully!"});
        }

        [HttpGet("/{token}/csv")]
        public ActionResult GetCsvFiles(string token)
        {
            User user = Authenticator.Tokens[token];
            return Ok(new {CsvFiles = _manager.GetCsvFiles(user.Username)});
        }
    }
}