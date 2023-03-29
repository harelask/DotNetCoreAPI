using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace corewebapi.Models
{
    public class Product_Master
    {
        [Key]
        public int Product_id {get;set;}
        [ForeignKey("Category")]
        public int Category_id { get; set; }
        public string Product_Description { get; set; }
        public string UOM { get; set; }
        public decimal rate { get; set; }
        public string active { get; set; }
    }
}
