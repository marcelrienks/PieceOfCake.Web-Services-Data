using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Scrummage.Interfaces;

//todo: investigate creating unit tests for the repository, by creating a fake contaxt/dbset class, which can be dependancy injected in?
namespace Scrummage.DataAccess {
	public class Repository<TModel> : IRepository<TModel> where TModel : class {

		#region Properties
		private readonly Context Context;
		private bool Disposed;
		#endregion

		public Repository(Context context) {
			Context = context;
		}

		#region Members
		/// <summary>
		/// This Gets all entries of type TEntity.
		/// </summary>
		/// <returns>List<TEntity></returns>
		public List<TModel> All() {
			return Context.Set<TModel>().ToList();
		}

		/// <summary>
		/// This executes a where claus, based on a function passed in
		/// </summary>
		/// <param name="query"></param>
		/// <returns>List<TEntity></returns>
		public List<TModel> Where(Func<TModel, bool> query) {
			return Context.Set<TModel>().Where(query).ToList();
		}

		/// <summary>
		/// This applies an Order By clause, based on the function passed in
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="orderBy"></param>
		/// <returns>List<TEntity></returns>
		public List<TModel> OrderBy<TKey>(Func<TModel, TKey> orderBy) {
			return Context.Set<TModel>().OrderBy(orderBy).ToList();
		}

		/// <summary>
		/// This executes a where claus and applies an Order By clause, based on a functions passed in
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="query"></param>
		/// <param name="orderBy"></param>
		/// <returns>List<TEntity></returns>
		public List<TModel> WhereOrderBy<TKey>(Func<TModel, bool> query, Func<TModel, TKey> orderBy) {
			return Context.Set<TModel>().Where(query).OrderBy(orderBy).ToList();
		}

		/// <summary>
		/// This Finds a specific entry of type TEntity, by id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>TEntity</returns>
		public TModel Find(int id) {
			return Context.Set<TModel>().Find(id);
		}

		/// <summary>
		/// This Creates a new entry of type TEntity.
		/// </summary>
		/// <param name="entity"></param>
		public void Create(TModel entity) {
			Context.Set<TModel>().Add(entity);
		}

		/// <summary>
		/// This updates the specified entry of type TEntity.
		/// </summary>
		/// <param name="entity"></param>
		public void Update(TModel entity) {
			Context.Entry(entity).State = EntityState.Modified;
		}

		/// <summary>
		/// This deletes a specific entry of type TEntity, by id.
		/// </summary>
		/// <param name="id"></param>
		public void Delete(int id) {
			var entity = Context.Set<TModel>().Find(id);
			Context.Set<TModel>().Remove(entity);
		}

		/// <summary>
		/// This saves all changes made to an entry of type TEntity (created, updated, or deleted)
		/// </summary>
		public void Save() {
			Context.SaveChanges();
		}

		//Async Example
		//Async Method, called from an Async Controller Method (See Role Repository and IRepository for following code)
		//public Task<TModel> FindAsync(int id) {
		//	return Context.Set<TModel>().FindAsync(id);
		//}
		#endregion

		#region IDisposable Members
		protected virtual void Dispose(bool disposing) {
			if (!Disposed) {
				if (disposing) {
					Context.Dispose();
				}
			}
			Disposed = true;
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}