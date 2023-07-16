using Newtonsoft.Json;
using TarefasApp.Services.Helpers;
using TarefasApp.Services.Model.Responses;

namespace TarefasApp.UI.Models
{
    public class TarefasConsultaViewModel
    {
        public List<TarefasConsultaResponseModel> Tarefas { get; set; }

        public TarefasConsultaViewModel()
        {
            InitializeAsync().Wait();
        }

        private async Task InitializeAsync()
        {
            Tarefas = await GetTarefasAsync();
        }

        private async Task<List<TarefasConsultaResponseModel>> GetTarefasAsync()
        {
            var auth = await SecureStorage.GetAsync("auth_user");
            var user = JsonConvert.DeserializeObject<AutenticarResponseModel>(auth);

            var servicesHelper = new ServicesHelper();
            return await servicesHelper.Get<List<TarefasConsultaResponseModel>>("tarefas");
        }
    }
}
