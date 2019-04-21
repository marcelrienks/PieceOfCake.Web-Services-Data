using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRepository<TModel> : IDisposable where TModel : class
    {
        #region Synchronous

        IQueryable<TModel> All();
        TModel Find(int id);
        IEnumerable<TModel> Where(Func<TModel, bool> query);
        IOrderedEnumerable<TModel> OrderBy<TKey>(Func<TModel, TKey> orderBy);
        IOrderedEnumerable<TModel> WhereOrderBy<TKey>(Func<TModel, bool> query, Func<TModel, TKey> orderBy);
        int Create(TModel entity);
        int Update(TModel entity);
        int Delete(TModel entity);
        bool Exists(int id);

        #endregion

        #region Asynchronous

        Task<TModel> FindAsync(int id);
        Task<int> CreateAsync(TModel entity);
        Task<int> UpdateAsync(TModel entity);
        Task<int> DeleteAsync(TModel entity);
        Task<bool> ExistsAsync(int id);

        #endregion
    }
}