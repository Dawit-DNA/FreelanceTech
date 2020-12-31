using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.Models
{
    public interface IJobRepository
    {
        bool PostJob(Job job);
    }
}
