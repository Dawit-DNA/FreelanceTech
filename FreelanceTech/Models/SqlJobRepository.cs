using FreelanceTech.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelanceTech.Models
{
    public class SqlJobRepository : IJobRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlJobRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool PostJob(Job job)
        {
            dbContext.Add(job);
            dbContext.SaveChanges();
            return true;
        }
    }
}
