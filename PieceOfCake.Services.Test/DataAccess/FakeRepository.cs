using PieceOfCake.Data.Interfaces;
using PieceOfCake.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieceOfCake.Services.Test.DataAccess
{
    public class FakeRepository<TModel> : IRepository<TModel> where TModel : class
    {
        #region Properties

        private bool _disposed;

        public IEnumerable<TModel> ModelList { get; set; }
        public TModel Model { get; set; }
        public bool IsCreated { get; private set; }
        public bool IsUpdated { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsSaved { get; private set; }

        #endregion

        public FakeRepository()
        {
            IsCreated = false;
            IsSaved = false;
        }

        #region Members

        #region Synchronous

        public IQueryable<TModel> All()
        {
            return ModelList.AsQueryable();
        }

        public TModel Find(int id)
        {
            if (ModelList == null)
            {
                return null;
            }

            //Based on type of TEntity, convert to specified type and find entry using id (All this logic is required to use the Find() function with id field)
            object result;
            if (typeof(TModel) == typeof(Role))
            {
                result = ModelList.Cast<Role>().ToList().Find(role => role.Id == id);
            }
            else if (typeof(TModel) == typeof(User))
            {
                result = ModelList.Cast<User>().ToList().Find(user => user.Id == id);
            }
            else
            {
                throw new NotImplementedException(
                    "Custom Exception: Convertion of type TEntity to model is not implemented. Extend conditional statement in FakeRepository => Find(int id)");
            }

            //Convert back to TEntity and return
            return (TModel)Convert.ChangeType(result, typeof(TModel));
        }

        public IEnumerable<TModel> Where(Func<TModel, bool> query)
        {
            return ModelList;
        }

        public IOrderedEnumerable<TModel> OrderBy<TKey>(Func<TModel, TKey> orderBy)
        {
            return ModelList.OrderBy(orderBy);
        }

        public IOrderedEnumerable<TModel> WhereOrderBy<TKey>(Func<TModel, bool> query, Func<TModel, TKey> orderBy)
        {
            return ModelList.Where(query).OrderBy(orderBy);
        }

        public int Create(TModel entity)
        {
            IsCreated = true;
            IsSaved = true;
            return 1;
        }

        public int Update(TModel entity)
        {
            IsUpdated = true;
            IsSaved = true;
            return 1;
        }

        public int Delete(TModel entity)
        {
            IsDeleted = true;
            IsSaved = true;
            return 1;
        }

        public bool Exists(int id)
        {
            if (typeof(TModel) == typeof(User))
            {
                var users = ModelList as IEnumerable<User>;
                return users != null && users.Count(entity => entity.Id == id) > 0;
            }

            if (typeof(TModel) == typeof(Role))
            {
                var roles = ModelList as IEnumerable<Role>;
                return roles != null && roles.Count(entity => entity.Id == id) > 0;
            }

            return false;
        }

        #endregion

        #region Asynchronous

        public Task<TModel> FindAsync(int id)
        {
            return Task<TModel>.Factory.StartNew(() => Find(id));
        }

        public Task<int> CreateAsync(TModel entity)
        {
            return Task<int>.Factory.StartNew(() => Create(entity));
        }

        public Task<int> UpdateAsync(TModel entity)
        {
            return Task<int>.Factory.StartNew(() => Update(entity));
        }

        public Task<int> DeleteAsync(TModel entity)
        {
            return Task<int>.Factory.StartNew(() => Delete(entity));
        }

        public Task<bool> ExistsAsync(int id)
        {
            return Task<bool>.Factory.StartNew(() => Exists(id));
        }

        #endregion

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //There is nothing to dispose of in the fake repository
                }
            }
            _disposed = true;
        }

        #endregion
    }
}