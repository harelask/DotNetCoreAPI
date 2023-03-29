using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace corewebapi.Models
{
    public class studentMark
    {
        [Key]
        public int markId { get; set; }
        public int studentId { get; set; }

        public int tamil { get; set; }
        public int english { get; set; }
        public int maths { get; set; }
        public int science { get; set; }
        public int social { get; set; }
       
          
    }
}
