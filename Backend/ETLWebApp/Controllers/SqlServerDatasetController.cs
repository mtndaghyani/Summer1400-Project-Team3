using System.Collections.Generic;
using ETLLibrary.Authentication;
using ETLLibrary.Database;
using ETLLibrary.Database.Utils;
using ETLLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ETLWebApp.Controllers
{
    [ApiController]
    [Route("dataset/sqlserver/")]
    public class SqlServerDatasetController : Controller
    {
        private ISqlServerDatasetManager _manager;

        public SqlServerDatasetController(ISqlServerDatasetManager manager)
        {
            _manager = manager;
        }
        
        [HttpPost("create/")]
        public ActionResult Create(string token, DatasetInfo model)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            _manager.CreateDataset(user.Username, model);
            return Ok(new {Message = "Dataset added successfully."});
        }

        [HttpDelete("delete/{name}")]
        public ActionResult Delete(string token, string name)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            _manager.DeleteDataset(name, user);
            return Ok(new {Message = $"Dataset {name} deleted successfully."});
        }

        [Route("{name}")]
        [HttpGet]
        public ActionResult GetContent(string token, string name)
        {
            //TODO:
            return null;
        }
        
        [HttpGet("tables/")]
        public ActionResult GetTables( string dbName, string dbUsername, string dbPassword, string url)
        {
            var result = _manager.GetTables(dbName, dbUsername, dbPassword, url);
            if (result == null)
            {
                return BadRequest(new {Message = "Database not found"});
            }
            else
            {
                return Ok(new {TableNames = result});
            }
        }
    }
}