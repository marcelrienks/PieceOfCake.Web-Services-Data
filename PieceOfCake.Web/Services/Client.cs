using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PieceOfCake.Web.Services
{
    public class Client : IDisposable
    {
        #region Properties

        public HttpClient HttpClient { get; set; }

        #endregion

        /// <summary>
        /// Instantioate Http Client
        /// </summary>
        public Client()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("ServiceUri"))
            };
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Dispose Http Client
        /// </summary>
        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}