using BLL.Admin.Models;
using BLL.Enums;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Data.Repositories
{
    public interface ISettingRepository
    {
        Setting GetSetting(Settings setting);
        Task<Setting> FindSetting(Expression<Func<Setting, bool>> predicate);
    }
}