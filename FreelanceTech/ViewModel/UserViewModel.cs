using FreelanceTech.Models;
using FreelanceTech.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.ViewModel
{
    public class UserViewModel
    {
        public static string customerJob = null;
        public Freelancer freelancer { get; set; }
        public Customer customer { get; set; }
        public Job job { get; set; }
        public Proposal proposal { get; set; }
        public Chat chat { get; set; }
        public CustomerViewModel customerViewModel { get; set; }
        public FreelancerViewModel FreelancerViewModel { get; set; }
        public List<FreelancerViewModel> freelancers;
        public List<Job> jobs;
        public List<ProposalViewModel> proposals;
    }
}
