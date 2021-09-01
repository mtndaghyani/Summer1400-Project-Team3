using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETLLibrary.Database
{
    public class Csv
    {
        [Key] public int Id{ get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
    }
}