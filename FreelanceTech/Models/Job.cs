using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.Models
{
    public class Job
{
    [Key]
    public int Job_id { get; set; }
    public string Type { get; set; }
    public DateTime Start_Date { get; set; }
    public DateTime End_Date { get; set; }
    public string Level { get; set; }
    public double Payment_Amount { get; set; }
    public string Status { get; set; }
    
    [ForeignKey("Customer")]
    public int Customer_ID { get; set; }
    
    [ForeignKey("Freelancer")]
    public int Freelancer_ID { get; set; }
    public string Review { get; set; }

    }
}
