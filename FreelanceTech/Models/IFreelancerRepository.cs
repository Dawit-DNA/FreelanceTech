using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.Models
{
    interface IFreelancerRepository
    {
        IEnumerable<Freelancer> GetAllFreelancers();
        IEnumerable<Freelancer> Search(string searchTerm);
        bool Update(Freelancer freelancer);
    }
}
