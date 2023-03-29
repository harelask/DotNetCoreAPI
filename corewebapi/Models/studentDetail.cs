using System.ComponentModel.DataAnnotations;

namespace corewebapi.Models
{
    public class studentDetail
    {
        [Key]
        public int stuId { get; set; }
        public string regNo { get; set; }
        public string studentName { get; set; }
        public DateTime DOB { get; set; }
        public string gender { get; set; }
        public string istrue { get; set; }
    }
}
