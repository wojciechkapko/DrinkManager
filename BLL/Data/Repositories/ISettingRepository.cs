using BLL.Admin.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Data.Repositories
{
    public interface ISettingRepository
    {
        Setting GetSettingById(int id);
        Task<Setting> FindSetting(Expression<Func<Setting, bool>> predicate);
    }
}