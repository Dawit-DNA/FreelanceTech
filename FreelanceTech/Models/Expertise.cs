using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceTech.Models
{
    
    public class Expertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string freelancerId { get; set; }
        public int category { get; set; }
        public int skill  { get; set; }
        public string status { get; set; }
        public string level { get; set; }
    }
}
