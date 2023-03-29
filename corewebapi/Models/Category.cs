using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace corewebapi.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; } 

        public string CategoryName { get; set; }


    }
}
