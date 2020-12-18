using System.ComponentModel.DataAnnotations;


namespace FreelanceTech.Models
{
    
    public class Expertise
    {
        public int id { get; set; }
        public string freelancerId { get; set; }
        public string category { get; set; }
        public string skill  { get; set; }
        public string status { get; set; }
        public string level { get; set; }
    }
}
