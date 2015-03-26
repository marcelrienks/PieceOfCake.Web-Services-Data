using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PieceOfCake.Web.Interfaces
{
    public interface IService<TModel> : IDisposable where TModel : class
    {
        Task<ICollection<TModel>> AllAsync(string resource);
        TModel GetAsync(int id);
        void PutAsync(TModel model);
        void PostAsync(TModel model);
        void DeleteAsync(int id);
    }
}
