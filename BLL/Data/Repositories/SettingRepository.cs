using BLL.Admin.Models;
using BLL.Enums;
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

        public Setting GetSetting(Settings settings)
        {
            return _context.Settings.Find((int)settings);
        }

        public Task<Setting> FindSetting(Expression<Func<Setting, bool>> predicate)
        {
            return _context.Settings.FirstOrDefaultAsync(predicate);
        }
    }
}
