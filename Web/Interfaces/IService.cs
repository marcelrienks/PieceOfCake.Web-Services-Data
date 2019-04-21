using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface IService<TModel> : IDisposable where TModel : IModel
    {
        Task<ICollection<TModel>> AllAsync(TModel model);
        Task<TModel> GetAsync(int id, TModel model);
        void UpdateAsync(TModel model);
        void CreateAsync(TModel model);
        void DeleteAsync(int id, TModel model);
    }
}
