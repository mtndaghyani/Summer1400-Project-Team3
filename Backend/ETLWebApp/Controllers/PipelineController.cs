using System;
using System.Text;
using ETLLibrary.Authentication;
using ETLLibrary.Interfaces;
using ETLLibrary.Model.Pipeline;
using ETLWebApp.Models.PipelineModel;
using ETLWebApp.Models.YmlModels;
using Microsoft.AspNetCore.Mvc;
using MediaTypeHeaderValue = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

namespace ETLWebApp.Controllers
{
    [ApiController]
    [Route("pipeline/")]
    public class PipelineController : Controller
    {
        private IPipelineManager _pipelineManager;
        private IYmlManager _ymlManager;

        public PipelineController(IPipelineManager pipelineManager, IYmlManager ymlManager)
        {
            _pipelineManager = pipelineManager;
            _ymlManager = ymlManager;
        }

        [HttpPost("create/")]
        public ActionResult Create(string token, PipelineCreateModel model)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            var res = _pipelineManager.CreatePipeline(user.Username, model.Name, model.Content);
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

            _pipelineManager.DeletePipeline(name, user);
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

            var pipeline = _pipelineManager.GetDbPipeline(user, name);

            return Ok(new {Content = pipeline.Content});
        }

        [Route("yml/upload")]
        [HttpPost]
        public ActionResult Upload([FromForm] CreateYmlModel model, string token)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            try
            {
                _ymlManager.SaveYml(model.File.OpenReadStream(), model.Name, user.Username, model.File.Length);
            }
            catch (Exception e)
            {
                return Conflict(new {Message = "File upload failed due to unknown error(s)."});
            }

            return Ok(new {Message = "File uploaded successfully."});
        }


        [HttpPost("run/")]
        public ActionResult Run(string token, RunModel model)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            var pipeline = _pipelineManager.GetDbPipeline(user, model.Name);
            try
            {
                new PipelineConvertor(user.Username, pipeline.Content).Pipeline.Run();
            }
            catch (Exception e)
            {
                return StatusCode(500, new {Message = e.Message});
            }

            return Ok(new {Messsage = "Pipeline ran successfully!"});
        }


        [HttpGet("yml/download/{name}")]
        public ActionResult Download(string token, string name)
        {
            var user = Authenticator.GetUserFromToken(token);
            if (user == null)
            {
                return Unauthorized(new {Message = "First login."});
            }

            try
            {
                var yml = _ymlManager.GetYml(user.Id, name);
                return GenerateYmlFile(name, yml);
            }
            catch (Exception e)
            {
                return StatusCode(500, new {Message = e.Message});
            }
        }

        private FileContentResult GenerateYmlFile(string name, string yml)
        {
            return new(Encoding.UTF8.GetBytes(yml),
                new MediaTypeHeaderValue("text/yaml"))
            {
                FileDownloadName = $"{name}.yml"
            };
        }
    }
}