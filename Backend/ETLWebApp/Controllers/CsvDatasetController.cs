using System;
using ETLLibrary.Authentication;
using ETLLibrary.Database.Utils;
using ETLLibrary.Interfaces;
using ETLWebApp.Models.CsvModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public ActionResult Create([FromForm] CreateModel model, string token)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            var details = GetCreateModelDetails(model.Details);
            var info = new CsvInfo()
            {
                Name = details.Name,
                ColDelimiter = details.ColDelimiter,
                RowDelimiter = details.RowDelimiter,
                HasHeader = details.HasHeader == "true"
            };
            try
            {
                _manager.SaveCsv(model.File.OpenReadStream(), user.Username, model.File.FileName, info, model.File.Length);
            }
            catch (Exception e)
            {
                return Conflict(new {Message = e.Message});
            }
            return Ok(new {Message = "File uploaded successfully."});
        }

        private CreateModelDetails GetCreateModelDetails(string modelDetails)
        {
            return JsonConvert.DeserializeObject<CreateModelDetails>(modelDetails);
        }

        [Route("{name}")]
        [HttpGet]
        public ActionResult GetContent(string name, string token)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            var response = _manager.GetCsvContent(user, name);
            if (response == null)
            {
                return NotFound(new {Message = "Not Found"});
            }

            return Ok(new {Content = response});
        }

        [HttpDelete("delete/{name}")]
        public ActionResult Delete(string name, string token)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            _manager.DeleteCsv(user, name);
            return Ok(new {Message = "File deleted successfully."});
        }
    }
}