using System.ComponentModel.DataAnnotations;

namespace corewebapi.Models
{
    public class Usermaster
    {
        [Key]
        public int userid { get; set; }
        public string usercode { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public char active { get; set; }
    }
}
