using System;
using ETLLibrary.Authentication;
using ETLLibrary.Database;
using ETLLibrary.Database.Utils;
using ETLLibrary.Interfaces;
using ETLWebApp.Models.CsvModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace ETLWebApp.Controllers
{
    [ApiController]
    [Route("dataset/csv/")]
    public class CsvDatasetController : ControllerBase
    {
        private ICsvDatasetManager _manager;

        public CsvDatasetController(ICsvDatasetManager manager)
        {
            _manager = manager;
        }

        [HttpPost("create/")]
        public ActionResult Create([FromForm]CreateModel model, string token)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            var info = new CsvInfo()
            {
                ColDelimiter = model.ColDelimiter,
                RowDelimiter = model.RowDelimiter,
                HasHeader = model.HasHeader
            };
            _manager.SaveCsv(model.File.OpenReadStream(), user.Username, model.File.FileName, info, model.File.Length);
            return Ok(new {Message = "File uploaded successfully."});
        }

        [Route("{filename}")]
        [HttpGet]
        public ActionResult GetContent(string filename, string token)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            var response = _manager.GetCsvContent(user.Username, filename);
            if (response == null)
            {
                return NotFound(new {Message = "Not Found"});
            }

            return Ok(new {Content = response});
        }

        [HttpDelete("delete/{fileName}")]
        public ActionResult Delete(string fileName, string token)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            _manager.DeleteCsv(user, fileName);
            return Ok(new {Message = "File deleted successfully."});
        }
    }
}