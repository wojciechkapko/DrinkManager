using System.Threading.Tasks;

namespace BLL.Data.Repositories
{
    public interface IActivitiesRepository
    {
        Task AddActivity(UserActivity activity);
        Task<bool> SaveChanges();
    }
}
