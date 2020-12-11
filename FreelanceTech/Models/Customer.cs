using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreelanceTech.Models
{
    public class Customer
{
        [Key]
        public string customerId { get; set; }
        public byte[] photo { get; set; }
        public byte[] legalId { get; set; }
        public string status { get; set; }
        public int language { get; set; }
        public int phoneNumber { get; set; }
        public Address address { get; set; }
        public Language Language { get; set; }
        public Wallet Wallet { get; set; }
        public List<Transaction> Transaction { get; set; }
        public List<Job> Job { get; set; }
        public string englishProficiency { get; set; }
    }
}
