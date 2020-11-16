using BLL.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Data.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly DrinkAppContext _context;

        public SettingRepository(DrinkAppContext context)
        {
            _context = context;
        }

        public Setting GetSettingById(int id)
        {
            return _context.Settings.Find(id);
        }

        public Task<Setting> FindSetting(Expression<Func<Setting, bool>> predicate)
        {
            return _context.Settings.FirstOrDefaultAsync(predicate);
        }
    }
}
