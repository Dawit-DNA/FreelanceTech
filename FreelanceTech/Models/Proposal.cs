using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FreelanceTech.Models
{
    public class Proposal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string proposalId { get; set; }
        public string jobId { get; set; }
        public Job Job { get; set; }
        public string freelancerId { get; set; }
        public Freelancer Freelancer { get; set; }
        public string description { get; set; }
        public double bidAmount { get; set; }
        public string answers { get; set; }

    }
}
