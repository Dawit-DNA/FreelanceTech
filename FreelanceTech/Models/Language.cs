using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FreelanceTech.Models
{
    public class Language
    {
        public int id { get; set; }
        public string language { get; set; }
        public string userId { get; set; }
    }
}
