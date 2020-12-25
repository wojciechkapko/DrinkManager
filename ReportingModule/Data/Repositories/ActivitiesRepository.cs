using BLL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReportingModuleApi.Data.Repositories
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

        public async Task<List<UserActivity>> Get(Expression<Func<UserActivity, bool>> query)
        {
            var actions = await this._context.UserActivities.Where(query).ToListAsync();

            return actions;
        }

        public async Task<List<UserActivity>> Get()
        {
            var actions = await this._context.UserActivities.ToListAsync();

            return actions;
        }

        public async Task<UserActivity> Get(string id)
        {
            var actions = await this._context.UserActivities.FindAsync(id);

            return actions;
        }
        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
