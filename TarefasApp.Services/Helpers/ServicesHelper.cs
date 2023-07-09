using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Método genérico para requisições do tipo POST
        /// </summary>        
        public async Task<TResponse> Post<TRequest, TResponse>(string endpoint, TRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                var result = await httpClient.PostAsync($"{AppSettings.BaseUrl}{endpoint}", content);

                var builder = new StringBuilder();
                using (var r = result.Content)
                {
                    var task = r.ReadAsStringAsync();
                    builder.Append(task.Result);
                }

                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TResponse>(builder.ToString());
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ErrorResult>(builder.ToString());
                    throw new Exception(error?.Message);
                }
            }            
        }
    }
}
