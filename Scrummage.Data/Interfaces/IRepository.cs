using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieceOfCake.Data.Interfaces
{
    public interface IRepository<TModel> : IDisposable where TModel : class
    {
        IQueryable<TModel> All();
        TModel Find(int id);
        Task<TModel> FindAsync(int id);
        IEnumerable<TModel> Where(Func<TModel, bool> query);
        IOrderedEnumerable<TModel> OrderBy<TKey>(Func<TModel, TKey> orderBy);
        IOrderedEnumerable<TModel> WhereOrderBy<TKey>(Func<TModel, bool> query, Func<TModel, TKey> orderBy);
        int Create(TModel entity);
        Task<int> CreateAsync(TModel entity);
        int Update(TModel entity);
        Task<int> UpdateAsync(TModel entity);
        int Delete(TModel entity);
        Task<int> DeleteAsync(TModel entity);
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);
    }
}