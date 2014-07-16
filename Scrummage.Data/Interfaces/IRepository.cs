using System;
using System.Collections.Generic;
using System.Linq;

namespace Scrummage.Data.Interfaces
{
    public interface IRepository<TModel> : IDisposable where TModel : class
    {
        IQueryable<TModel> All();
        TModel Find(int id);
        IEnumerable<TModel> Where(Func<TModel, bool> query);
        IOrderedEnumerable<TModel> OrderBy<TKey>(Func<TModel, TKey> orderBy);
        IOrderedEnumerable<TModel> WhereOrderBy<TKey>(Func<TModel, bool> query, Func<TModel, TKey> orderBy);
        void Create(TModel entity);
        void Update(TModel entity);
        void Delete(TModel entity);
        bool Exists(int id);

        //Async Example
        //Async Method structure (See Role Repository and RoleController for following code)
        //System.Threading.Tasks.Task<TModel> FindAsync(int id);
    }
}