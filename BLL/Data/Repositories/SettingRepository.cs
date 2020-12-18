using BLL.Admin.Models;
using BLL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Setting> GetAllSettings()
        {
            return _context.Settings.AsEnumerable();
        }

        public Setting GetSetting(Settings settings)
        {
            return _context.Settings.Find((int)settings);
        }

        public void SetSetting(Settings settings, string value)
        {
            _context.Settings.Find((int)settings).Value = value;
            _context.SaveChanges();
        }

        public Task<Setting> FindSetting(Expression<Func<Setting, bool>> predicate)
        {
            return _context.Settings.FirstOrDefaultAsync(predicate);
        }

        public void Update(Setting setting)
        {
            _context.Settings.Update(setting);
            _context.SaveChanges();
        }
    }
}
