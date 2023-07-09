using Syncfusion.Maui.DataForm;
using TarefasApp.Services.Helpers;
using TarefasApp.Services.Model.Responses;
using TarefasApp.Services.Models.Requests;

namespace TarefasApp.UI.Behaviors
{
    /// <summary>
    /// Classe para definir os comportamentos da página de criação de conta
    /// </summary>
    public class CriarContaBehavior : Behavior<ContentPage>
    {
        //atributos
        private SfDataForm _formCriarConta;
        private Button _btnCriarConta;

        /// <summary>
        /// Método para implementar os eventos da página
        /// </summary>
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            #region Fazendo a captura dos elementos da página

            _formCriarConta = bindable.FindByName<SfDataForm>("formCriarConta");
            _btnCriarConta = bindable.FindByName<Button>("btnCriarConta");

            #endregion

            #region Criando os eventos da página

            _btnCriarConta.Clicked += OnBtnCriarContaClicked;

            #endregion
        }

        private async void OnBtnCriarContaClicked(object sender, EventArgs e)
        {
            //verificando se o formulário não possui erros de validação
            if (_formCriarConta.Validate())
            {
                try
                {
                    //capturar os dados do formulário
                    var model = (CriarContaRequestModel)_formCriarConta.DataObject;

                    //enviando a requisição para a API
                    var servicesHelper = new ServicesHelper();
                    var result = await servicesHelper.Post<CriarContaRequestModel, CriarContaResponseModel>("criar-conta", model);

                    await App.Current.MainPage.DisplayAlert(
                        "Sucesso!",
                        $"Parabéns {result.Nome}, sua conta de usuário foi cadastrada com sucesso!",
                        "OK");

                    await Shell.Current.GoToAsync("///MainPage");
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



