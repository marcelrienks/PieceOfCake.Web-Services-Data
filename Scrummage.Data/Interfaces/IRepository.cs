using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrummage.Data.Interfaces
{
    public interface IRepository<TModel> : IDisposable where TModel : class
    {
        IQueryable<TModel> All();
        TModel Find(int id);
        Task<TModel> FindAsync(int id);
        IEnumerable<TModel> Where(Func<TModel, bool> query);
        IOrderedEnumerable<TModel> OrderBy<TKey>(Func<TModel, TKey> orderBy);
        IOrderedEnumerable<TModel> WhereOrderBy<TKey>(Func<TModel, bool> query, Func<TModel, TKey> orderBy);
        void Create(TModel entity);
        void CreateAsync(TModel entity);
        void Update(TModel entity);
        void UpdateAsync(TModel entity);
        void Delete(TModel entity);
        void DeleteAsync(TModel entity);
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);
    }
}