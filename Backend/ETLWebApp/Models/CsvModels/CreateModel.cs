using Microsoft.AspNetCore.Http;

namespace ETLWebApp.Models.CsvModels
{
    public class CreateModel
    {
        public string Name { get; set; }
        public string ColDelimiter { get; set; }
        public string RowDelimiter { get; set; }
        public bool HasHeader { get; set; }
        public IFormFile File { get; set; }
    }
}