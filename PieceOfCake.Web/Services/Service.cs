using PieceOfCake.Web.Interfaces;
using PieceOfCake.Web.Libraries;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PieceOfCake.Web.Services
{
    public class Service<TModel> : IService<TModel> where TModel : class
    {
        #region Properties

        private readonly Client _client;
        private bool _disposed;

        #endregion

        public Service(Client client)
        {
            _client = client;
        }

        #region Methods

        public async Task<ICollection<TModel>> AllAsync(string resource)
        {
            try
            {
                var response = await _client.HttpClient.GetAsync(RestResourceUri.FormatResourceAsUri(resource));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<ICollection<TModel>>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TModel GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void PutAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public void PostAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Methods

        void IDisposable.Dispose()
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
                    _client.Dispose();
                }
            }
            _disposed = true;
        }

        #endregion
    }
}