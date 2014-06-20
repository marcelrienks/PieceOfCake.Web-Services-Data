using System;
using System.Collections.Generic;
using System.Linq;
using Scrummage.Interfaces;
using Scrummage.Models;

//Todo: Investigate suing Mock DbSet instead of having a fake Repository
namespace Scrummage.Test.DataAccess {
	public class FakeRepository<TModel> : IRepository<TModel> where TModel : class {

		#region Properties
		private bool _disposed;

		public List<TModel> ModelList { get; set; }
		public TModel Model { get; set; }
		public bool IsCreated { get; private set; }
		public bool IsUpdated { get; private set; }
		public bool IsDeleted { get; private set; }
		public bool IsSaved { get; private set; }
		#endregion

		public FakeRepository() {
			IsCreated = false;
			IsSaved = false;
		}

		#region Members
		public List<TModel> All() {
			return ModelList;
		}

		public List<TModel> Where(Func<TModel, bool> query) {
			return ModelList;
		}

		public List<TModel> OrderBy<TKey>(Func<TModel, TKey> orderBy) {
			return ModelList;
		}

		public List<TModel> WhereOrderBy<TKey>(Func<TModel, bool> query, Func<TModel, TKey> orderBy) {
			return ModelList;
		}

		public TModel Find(int id) {
			if (ModelList == null) {
				return null;
			}

			object result;
			//Based on type of TEntity, convert to specified type and find entry using id (All this logic is required to use the Find() function with id field)
			if (typeof(TModel) == typeof(Role)) {
				result = ModelList.Cast<Role>().ToList().Find(role => role.RoleId == id);

			} else if (typeof(TModel) == typeof(Member)) {
				result = ModelList.Cast<Member>().ToList().Find(member => member.MemberId == id);

			} else {
				throw new NotImplementedException("Custom Exception: Convertion of type TEntity to model is not implemented. Extend conditional statement in FakeRepository => Find(int id)");
			}

			//Convert back to TEntity and return
			return (TModel)Convert.ChangeType(result, typeof(TModel));
		}

		public void Create(TModel entity) {
			IsCreated = true;
			IsSaved = true;
		}

		public void Update(TModel entity) {
			IsUpdated = true;
			IsSaved = true;
		}

		public void Delete(int id) {
			IsDeleted = true;
			IsSaved = true;
		}

		public void Save() {
			IsSaved = true;
		}

		//Async Example
		//Async Method, called from an Async Controller Method (See Role Repository and IRepository for following code)
		//public System.Threading.Tasks.Task<TModel> FindAsync(int id) {
		//	throw new NotImplementedException();
		//}
		#endregion

		#region IDisposable Members
		protected virtual void Dispose(bool disposing) {
			if (!_disposed) {
				if (disposing) {
					//There is nothing to dispose of in the fake repository
				}
			}
			_disposed = true;
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}