using Microsoft.EntityFrameworkCore;

namespace BLL.Data
{
    public class ReportingApiContext : DbContext
    {
        public ReportingApiContext(DbContextOptions<ReportingApiContext> options) : base(options)
        {
        }
        public DbSet<UserActivity> UserActivities { get; set; }

        public void CheckIfReportingDatabaseExistsAndCreateIfNot()
        {
            Database.Migrate();
        }
    }
}
