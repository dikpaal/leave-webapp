using Microsoft.EntityFrameworkCore;
using leave_webapp.Models;

namespace leave_webapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LeaveApplication> LeaveApplications { get; set; }
    }
}
