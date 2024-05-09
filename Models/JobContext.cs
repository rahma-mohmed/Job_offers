using Microsoft.EntityFrameworkCore;

namespace Job_offers.Models
{
    public class JobContext : DbContext
    {
        public JobContext() : base()
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<RoleViewModel> Roles { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=JobDB ;Integrated Security=True");
        }
    }
}
