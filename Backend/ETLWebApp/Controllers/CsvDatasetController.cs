using ETLLibrary.Authentication;
using ETLLibrary.Database;
using ETLLibrary.Interfaces;
using ETLWebApp.Models.CsvModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETLWebApp.Controllers
{
    [ApiController]
    [Route("dataset/csv/")]

    public class CsvDatasetController : ControllerBase
    {
        private EtlContext _context;
        private ICsvDatasetManager _manager;

        public CsvDatasetController(EtlContext context, ICsvDatasetManager manager)
        {
            _context = context;
            _manager = manager;
        }
        
        [HttpPost("create/")]
        public ActionResult Create(IFormFile myFile, string token)
        {
            User user = Authenticator.Tokens[token];
            _manager.SaveCsv(myFile.OpenReadStream(), user.Username, myFile.FileName, myFile.Length);
            return Ok(new {Message = "File uploaded successfully."});
        }

        [Route("content/")]
        [HttpGet]
        public ActionResult GetContent(ContentModel model)
        {
            User user = Authenticator.Tokens[model.Token];
            var response = _manager.GetCsvContent(user.Username, model.FileName);
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