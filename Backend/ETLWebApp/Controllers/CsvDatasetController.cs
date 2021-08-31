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
        public void Create([FromBody] IFormFile myFile, [FromBody] string token)
        {
            User user = Authenticator.Tokens[token];
            _manager.SaveCsv(myFile.OpenReadStream(), user.Username, myFile.FileName, myFile.Length);
        }
    }
}