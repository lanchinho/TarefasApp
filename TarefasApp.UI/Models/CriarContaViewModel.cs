using TarefasApp.Services.Models.Requests;

namespace TarefasApp.UI.Models
{
    public class CriarContaViewModel
    {
        //Acesso ao modelo do formulário
        public CriarContaRequestModel CriarContaRequestModel { get; set; }

        public CriarContaViewModel()
        {
            CriarContaRequestModel = new CriarContaRequestModel();
        }
    }
}
