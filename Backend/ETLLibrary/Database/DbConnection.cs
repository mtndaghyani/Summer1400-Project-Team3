using System.ComponentModel.DataAnnotations;

namespace ETLLibrary.Database
{
    public class DbConnection
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        
        public User User { get; set; }
        public int UserId { get; set; }
    }
}