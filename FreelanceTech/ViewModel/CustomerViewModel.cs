using FreelanceTech.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.ViewModel
{
    [Keyless]
    public class CustomerViewModel
    {
        public string customerId { get; set; }
        public string photo { get; set; }
        public string legalId { get; set; }
        public int status { get; set; }
        public int language { get; set; }
        public string phoneNumber { get; set; }
        /*        public Address address { get; set; }
                public Language Language { get; set; }
                public Wallet Wallet { get; set; }
                public List<Transaction> Transaction { get; set; }
                public List<Job> Job { get; set; }*/
        public double amount { get; set; }
        public string category { get; set; }
        public string skills { get; set; }
        public string title { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public int englishProficiency { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Wallet wallet { get; set; }



    }
}
