using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceTech.Models;

namespace FreelanceTech.Models
{
    public class FreelancerUser
{
        public Expertise freelancer_skill;
        public int id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone_Number { get; set; }
        public string Expertise_Level { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Language { get; set; }
        public string Hourly_Rate { get; set; }
        public string Title { get; set; }
        public string Professional_Overview { get; set; }
        public byte[] Photo { get; set; }
        public string Address { get; set; }
        public byte[] Government_ID { get; set; }
        public int Rate { get; set; }
        public string Qualification_Status { get; set; }
    }
}
