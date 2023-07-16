using Syncfusion.Maui.DataForm;
using TarefasApp.Services.Helpers;
using TarefasApp.Services.Model.Requests;
using TarefasApp.Services.Model.Responses;

namespace TarefasApp.UI.Behaviors
{
    /// <summary>
    /// Classe para definir os comportamentos da página de recuperação de senha
    /// </summary>
    public class RecuperarSenhaBehavior : Behavior<ContentPage>
    {
        private SfDataForm _formRecuperarSenha;
        private Button _btnRecuperarSenha;

        /// <summary>
        /// Método para implementar os eventos da página
        /// </summary>
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            #region Capturar os elementos da página

            _formRecuperarSenha = bindable.FindByName<SfDataForm>("formRecuperarSenha");
            _btnRecuperarSenha = bindable.FindByName<Button>("btnRecuperarSenha");

            #endregion

            #region Criando os eventos da página

            _btnRecuperarSenha.Clicked += OnBtnRecuperarSenhaClicked;

            #endregion
        }

        private async void OnBtnRecuperarSenhaClicked(object sender, EventArgs e)
        {
            if (_formRecuperarSenha.Validate())
            {
                try
                {
                    var model = (RecuperarSenhaRequestModel)_formRecuperarSenha.DataObject;

                    var servicesHelper = new ServicesHelper();
                    var result = await servicesHelper
                        .Post<RecuperarSenhaRequestModel, RecuperarSenhaResponseModel>("recuperarsenha", model);

                    await App.Current.MainPage.DisplayAlert(
                        "Sucesso!",
                        $"Olá {result.Nome}, sua solicitação de recuperação de senha foi realizada com sucesso. Verifique a caixa de entrada do seu email.",
                        "OK");
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Aviso!", ex.Message, "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert
                    ("Aviso!", "Ocorreram erros de validação no preenchimento do formulário, por favor verifique.",
                    "OK");
            }
        }
    }
}



