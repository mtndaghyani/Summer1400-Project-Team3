using System.Net.Http.Json;
using ETLLibrary.Authentication;
using ETLLibrary.Interfaces;
using ETLWebApp.Models.PipelineModel;
using Microsoft.AspNetCore.Mvc;

namespace ETLWebApp.Controllers
{
    [ApiController]
    [Route("pipeline/")]
    public class PipelineController : Controller
    {
        private IPipelineManager _manager;

        public PipelineController(IPipelineManager manager)
        {
            _manager = manager;
        }

        [HttpPost("create/")]
        public ActionResult Create(string token, PipelineCreateModel model)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            var res = _manager.CreatePipeline(user.Username, model.Name, model.Content);
            return Ok(new {Message = res});
        }
        
        [HttpDelete("delete/{name}")]
        public ActionResult Delete(string token, string name)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            _manager.DeletePipeline(name, user);
            return Ok(new {Message = $"Pipeline {name} deleted successfully."});
        }

        [Route("{name}")]
        [HttpGet]
        public ActionResult GetPipelineContent(string token, string name)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            var pipeline = _manager.GetDbPipeline(user, name);

            return Ok(new {Content = pipeline.Content});
        }
        
        
    }
}