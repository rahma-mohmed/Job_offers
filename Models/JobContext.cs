using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Job_offers.Models
{
    public class JobContext : IdentityDbContext
    {
        public JobContext(DbContextOptions<JobContext> options) : base(options)
        {

        }

        public DbSet<ApplyForJob> ApplyForJobs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<RoleViewModel> Roles { get; set; }
    }
}
