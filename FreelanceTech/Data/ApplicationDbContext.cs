using System;
using System.Collections.Generic;
using System.Text;
using FreelanceTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Freelancer.Models;
using FreelancerTech.Models;

namespace FreelanceTech.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FreelanceTech.Models.FreelancerUser> FreelancerUser { get; set; }
        public DbSet<Freelancer.Models.Customer> Customer { get; set; }
        public DbSet<FreelancerTech.Models.Adminstrator> Adminstrator { get; set; }
        public DbSet<FreelanceTech.Models.Job> Job { get; set; }
        public DbSet<FreelanceTech.Models.Category> Category { get; set; }
        public DbSet<FreelanceTech.Models.Bid> Bid { get; set; }

    }
}
