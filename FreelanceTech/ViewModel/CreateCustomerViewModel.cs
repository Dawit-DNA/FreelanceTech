using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FreelanceTech.Models;

namespace FreelanceTech.ViewModel
{
    public class CreateCustomerViewModel
    {
        public string customerrId { get; set; }
        public string phoneNumber { get; set; }
    
        public IFormFile photo { get; set; }
        public string legaID { get; set; }
        public string status { get; set; }


        public string region { get; set; }
        public string city { get; set; }
        public string woreda { get; set; }
        public int houseNumber { get; set; }
        public int pobox { get; set; }
        public Constants.language language { get; set; }
        public Constants.englishProficiency englishProficiency { get; set; }
    }
}
