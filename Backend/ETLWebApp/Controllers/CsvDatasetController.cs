using System.Collections.Generic;
using ETLLibrary.Authentication;
using ETLLibrary.Database;
using ETLLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETLWebApp.Controllers
{
    [ApiController]
    [Route("dataset/csv")]
    public class CsvDatasetController : Controller
    {
        private EtlContext _context;
        private ICsvDatasetManager _manager;

        public CsvDatasetController(EtlContext context, ICsvDatasetManager manager)
        {
            _context = context;
            _manager = manager;
        }
        
        [HttpPost("/create")]
        public ActionResult Create([FromBody] IFormFile myFile, string token)
        {
            User user = Authenticator.Tokens[token];
            _manager.SaveCsv(myFile.OpenReadStream(), user.Username, myFile.FileName, myFile.Length);
            return Ok();
        }

        [HttpGet("/content")]
        public ActionResult GetContent(string token, string fileName)
        {
            User user = Authenticator.Tokens[token];
            var response = _manager.GetCsvContent(user.Username, fileName);
            if (response == null)
            {
                return NotFound(new {Message = "Not Found"});
            }
            else
            {
                return Ok(new {Content = response});
            }
        }
    }
}