using System.Threading.Tasks;

namespace BLL.Data.Repositories
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private readonly ReportingApiContext _context;
        public ActivitiesRepository(ReportingApiContext context)
        {
            _context = context;
        }
        public async Task AddActivity(UserActivity activity)
        {
            await _context.UserActivities.AddAsync(activity);
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
