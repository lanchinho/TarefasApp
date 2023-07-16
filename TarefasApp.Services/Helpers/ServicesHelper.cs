using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Services.Model.Responses;
using TarefasApp.Services.Settings;

namespace TarefasApp.Services.Helpers
{
    /// <summary>
    /// Classe para implementarmos os tipos de requisições Http 
    /// que poderão ser feitas para a API
    /// </summary>
    public class ServicesHelper
    {
        private AuthenticationHeaderValue? _authenticationsHeaderValue;        

        public ServicesHelper() { }

        public ServicesHelper(string accessToken)
        {
            _authenticationsHeaderValue = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        /// <summary>
        /// Método genérico para requisições do tipo POST
        /// </summary>        
        public async Task<TResponse> Post<TRequest, TResponse>(string endpoint, TRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                if (_authenticationsHeaderValue != null)
                    httpClient.DefaultRequestHeaders.Authorization = _authenticationsHeaderValue;

                var result = await httpClient.PostAsync($"{AppSettings.BaseUrl}{endpoint}", content);

                var response = ReadResponse(result);

                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TResponse>(response);
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ErrorResult>(response);
                    throw new Exception(error?.Message);
                }
            }
        }

        public async Task<TResponse> Get<TResponse>(string endpoint)
        {
            using (var httpClient = new HttpClient())
            {
                if (_authenticationsHeaderValue != null)
                    httpClient.DefaultRequestHeaders.Authorization = _authenticationsHeaderValue;

                var result = await httpClient.GetAsync($"{AppSettings.BaseUrl}{endpoint}");
                var response = ReadResponse(result);

                return JsonConvert.DeserializeObject<TResponse>(response);
            }
        }

        private static string ReadResponse(HttpResponseMessage result)
        {
            var builder = new StringBuilder();
            using (var r = result.Content)
            {
                var task = r.ReadAsStringAsync();
                builder.Append(task.Result);
            }

            return builder.ToString();
        }
    }
}
