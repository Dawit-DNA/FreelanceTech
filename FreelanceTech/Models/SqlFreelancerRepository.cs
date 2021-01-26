//using FreelanceTech.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace FreelanceTech.Models
//{
//    public class SqlFreelancerRepository : IFreelancerRepository
//    {
//        private readonly ApplicationDbContext dbContext;

//        public SqlFreelancerRepository(ApplicationDbContext dbContext)
//        {
//            this.dbContext = dbContext;
//        }
//        public IEnumerable<Freelancer> GetAllFreelancers()
//        {
//            return dbContext.Freelancer
//                .ToList();
//        }

//        public IEnumerable<Freelancer> Search(string searchTerm)
//        {
//            if (string.IsNullOrEmpty(searchTerm))
//            {
//                return dbContext.Freelancer;
//            }
//            return dbContext.Freelancer;
//            //return dbContext.Freelancer.Where(e => e..Contains(searchTerm) ||
//            //                                e.Expertise.category.Contains(searchTerm)).ToList();
//        }

//        public bool Update(Freelancer freelancer)
//        {
//            var newFreelancer = dbContext.Freelancer.Attach(freelancer);
//            newFreelancer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
//            dbContext.SaveChanges();
//            return true;
//        }
//    }
//}
