using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Data.Repositories
{
    public interface IActivitiesRepository
    {
        Task AddActivity(UserActivity activity);
        Task<bool> SaveChanges();
        Task<List<UserActivity>> Get();
        Task<UserActivity> Get(string id);
        Task<List<UserActivity>> Get(Expression<Func<UserActivity, bool>> query);
    }
}
