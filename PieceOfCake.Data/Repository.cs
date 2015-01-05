﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PieceOfCake.Data.Interfaces;
using PieceOfCake.Data.Models;
using RefactorThis.GraphDiff;
//Todo: investigate creating unit tests for the repository, by creating a fake contaxt/dbset class, which can be dependancy injected in?

namespace PieceOfCake.Data
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        #region Properties

        private readonly Context _context;
        private bool _disposed;

        #endregion

        public Repository(Context context)
        {
            _context = context;
        }

        #region Methods

        /// <summary>
        ///     This Gets all entries of type TModel.
        /// </summary>
        /// <returns>IQueryable<TModel/></returns>
        public IQueryable<TModel> All()
        {
            return _context.Set<TModel>();
        }

        /// <summary>
        ///     This Finds a specific entry of type TModel, by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TModel</returns>
        public TModel Find(int id)
        {
            return _context.Set<TModel>().Find(id);
        }

        /// <summary>
        ///     Asynchronously finds a specific entry of type TModel, by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task<TModel/></returns>
        public async Task<TModel> FindAsync(int id)
        {
            return await _context.Set<TModel>().FindAsync(id);
        }

        /// <summary>
        ///     This executes a where claus, based on a function passed in
        /// </summary>
        /// <param name="query"></param>
        /// <returns>IEnumerable<TModel/></returns>
        public IEnumerable<TModel> Where(Func<TModel, bool> query)
        {
            return _context.Set<TModel>().Where(query);
        }

        /// <summary>
        ///     This applies an Order By clause, based on the function passed in
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="orderBy"></param>
        /// <returns>IOrderedEnumerable<TModel/></returns>
        public IOrderedEnumerable<TModel> OrderBy<TKey>(Func<TModel, TKey> orderBy)
        {
            return _context.Set<TModel>().OrderBy(orderBy);
        }

        /// <summary>
        ///     This executes a where claus and applies an Order By clause, based on a functions passed in
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderBy"></param>
        /// <returns>IOrderedEnumerable<TModel/></returns>
        public IOrderedEnumerable<TModel> WhereOrderBy<TKey>(Func<TModel, bool> query, Func<TModel, TKey> orderBy)
        {
            return _context.Set<TModel>().Where(query).OrderBy(orderBy);
        }

        /// <summary>
        ///     This Creates a new entry of type TModel.
        /// </summary>
        /// <param name="entity"></param>
        public int Create(TModel entity)
        {
            _context.Set<TModel>().Add(entity);
            return _context.SaveChanges();
        }

        /// <summary>
        ///     Asynchronously Creates a new entry of type TModel.
        /// </summary>
        /// <param name="entity"></param>
        public async Task<int> CreateAsync(TModel entity)
        {
            _context.Set<TModel>().Add(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     This updates the specified entry of type TModel, and all it's relations.
        /// </summary>
        /// <param name="entity"></param>
        public int Update(TModel entity)
        {
            //Todo: determine weather or not updating a model and it's relations should be done, or rather have multiple API calls be made, or use hypermedia
            _context.Entry(entity).State = EntityState.Modified;
            //UpdateEntityGraph(entity);

            return _context.SaveChanges();
        }

        /// <summary>
        ///     Asynchronously updates the specified entry of type TModel, and all it's relations.
        /// </summary>
        /// <param name="entity"></param>
        public async Task<int> UpdateAsync(TModel entity)
        {
            //Todo: determine weather or not updating a model and it's relations should be done, or rather have multiple API calls be made, or use hypermedia
            _context.Entry(entity).State = EntityState.Modified;
            //UpdateEntityGraph(entity);

            return await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     This deletes a specific entry of type TEntity, by id.
        /// </summary>
        /// <param name="entity"></param>
        public int Delete(TModel entity)
        {
            _context.Set<TModel>().Remove(entity);
            return _context.SaveChanges();
        }

        /// <summary>
        ///     Asynchronously deletes a specific entry of type TEntity, by id.
        /// </summary>
        /// <param name="entity"></param>
        public async Task<int> DeleteAsync(TModel entity)
        {
            _context.Set<TModel>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     Checks if entity with specified id exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public bool Exists(int id)
        {
            if (typeof(TModel) == typeof(User))
            {
                return _context.Set<User>().Any(entity => entity.Id == id);
            }

            if (typeof(TModel) == typeof(Role))
            {
                return _context.Set<Role>().Any(entity => entity.Id == id);
            }

            return false;
        }

        /// <summary>
        ///     Asynchronously Checks if entity with specified id exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(int id)
        {
            if (typeof(TModel) == typeof(User))
            {
                return await _context.Set<User>().AnyAsync(entity => entity.Id == id);
            }

            if (typeof(TModel) == typeof(Role))
            {
                return await _context.Set<Role>().AnyAsync(entity => entity.Id == id);
            }

            return false;
        }

        private void UpdateEntityGraph(TModel entity)
        {
            if (entity is User)
            {
                _context.UpdateGraph(entity as User, map => map
                    .AssociatedCollection<User, Role>(user => user.Roles));
            }
            else if (entity is Role)
            {
                _context.UpdateGraph(entity as Role);
            }
        }

        #endregion

        #region IDisposable Methods

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
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion
    }
}