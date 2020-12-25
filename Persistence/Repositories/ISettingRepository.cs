using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface ISettingRepository
    {
        IEnumerable<Setting> GetAllSettings();
        Setting GetSetting(Settings setting);
        void SetSetting(Settings settings, string value);
        Task<Setting> FindSetting(Expression<Func<Setting, bool>> predicate);
        void Update(Setting setting);
    }
}