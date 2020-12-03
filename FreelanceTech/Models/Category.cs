using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        List<string> skills = new List<string>();
    }
}
