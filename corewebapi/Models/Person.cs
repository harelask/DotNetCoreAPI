using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace corewebapi.Models
{
    public class Person
    {
        [Key]
        public int perId {get;set;}
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public Int64 phone { get; set; }
    }
}
