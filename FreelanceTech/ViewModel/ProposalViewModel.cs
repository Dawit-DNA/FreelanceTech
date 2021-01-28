using FreelanceTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.ViewModel
{
    public class ProposalViewModel
    {
        public string proposalId { get; set; }
        public string jobId { get; set; }
        public Job job { get; set; }
        public string freelancerId { get; set; }
        public FreelancerViewModel freelancer;
        public string description { get; set; }
        public double bidAmount { get; set; }
        public string answers { get; set; }
        public string jobTitle { get; set; }

    }
}
