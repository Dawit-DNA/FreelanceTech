using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreelanceTech.Models
{
    public class Bid
    {

            public int id { get; set; }
            
            [ForeignKey("Job")]
            public int Job_id { get; set; }
            public string Last_Name { get; set; }
            public string Email { get; set; }
            public int Phone_Number { get; set; }
    
    }
}
