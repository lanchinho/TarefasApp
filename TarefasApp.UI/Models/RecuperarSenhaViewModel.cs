using TarefasApp.Services.Model.Requests;

namespace TarefasApp.UI.Models
{
    public class RecuperarSenhaViewModel
    {
        public RecuperarSenhaRequestModel RecuperarSenhaRequestModel { get; set; }

        public RecuperarSenhaViewModel()
        {
            RecuperarSenhaRequestModel = new RecuperarSenhaRequestModel();
        }
    }
}
