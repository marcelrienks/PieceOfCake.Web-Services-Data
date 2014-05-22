using System;
using System.Collections.Generic;

namespace Scrummage.Interfaces {
	public interface IRepository<TModel> : IDisposable where TModel : class {
		List<TModel> All();
		List<TModel> OrderBy<TKey>(Func<TModel, TKey> orderBy);
		List<TModel> Where(Func<TModel, bool> query);
		List<TModel> WhereOrderBy<TKey>(Func<TModel, bool> query, Func<TModel, TKey> orderBy);
		TModel Find(int id);
		void Create(TModel entity);
		void Update(TModel entity);
		void Delete(int id);

		//Async Example
		//Async Method structure (See Role Repository and RoleController for following code)
		//System.Threading.Tasks.Task<TModel> FindAsync(int id);
	}
}
