using Web.Interfaces;
using Web.Libraries;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Web.Services
{
    public class Service<TModel> : IService<TModel> where TModel : IModel
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

        public async Task<ICollection<TModel>> AllAsync(TModel model)
        {
            try
            {
                var response = await _client.HttpClient.GetAsync(RestResourceUri.FormatResourceAsUri(model.Resource()));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<ICollection<TModel>>();
            }
            catch (Exception ex)
            {
                //Todo: Need to handle exceptions in the Web.Service layer
                throw;
            }
        }

        public async Task<TModel> GetAsync(int id, TModel model)
        {
            try
            {
                var response = await _client.HttpClient.GetAsync(RestResourceUri.FormatResourceAsUri(model.Resource(), id));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<TModel>();
            }
            catch (Exception ex)
            {
                //Todo: Need to handle exceptions in the Web.Service layer
                throw;
            }
        }

        public async void UpdateAsync(TModel model)
        {
            try
            {
                var response = await _client.HttpClient.PutAsJsonAsync(RestResourceUri.FormatResourceAsUri(model.Resource()), model);
                response.EnsureSuccessStatusCode();
                //Todo: check if i need to test for a StatusCode with no content
            }
            catch (Exception ex)
            {
                //Todo: Need to handle exceptions in the Web.Service layer
                throw;
            }
        }

        public async void CreateAsync(TModel model)
        {
            try
            {
                var response = await _client.HttpClient.PostAsJsonAsync(RestResourceUri.FormatResourceAsUri(model.Resource()), model);
                response.EnsureSuccessStatusCode();
                //Todo: check if i need to test for a CreatedAtRoute with id
            }
            catch (Exception ex)
            {
                //Todo: Need to handle exceptions in the Web.Service layer
                throw;
            }
        }

        public async void DeleteAsync(int id, TModel model)
        {
            try
            {
                var response = await _client.HttpClient.DeleteAsync(RestResourceUri.FormatResourceAsUri(model.Resource(), id));
                response.EnsureSuccessStatusCode();
                //Todo: check if i need to test for a OK with content
            }
            catch (Exception ex)
            {
                //Todo: Need to handle exceptions in the Web.Service layer
                throw;
            }
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