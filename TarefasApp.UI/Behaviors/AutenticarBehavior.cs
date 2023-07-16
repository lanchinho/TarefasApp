using Newtonsoft.Json;
using Syncfusion.Maui.DataForm;
using TarefasApp.Services.Helpers;
using TarefasApp.Services.Model.Requests;
using TarefasApp.Services.Model.Responses;

namespace TarefasApp.UI.Behaviors
{
    public class AutenticarBehavior : Behavior<ContentPage>
    {
        private Button _btnAcesso;
        private SfDataForm _dataForm;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            //capturar o botão da página
            _btnAcesso = bindable.FindByName<Button>("btnAcesso");

            //criando um evento para o botão
            _btnAcesso.Clicked += OnButtonClicked;

            _dataForm = bindable.FindByName<SfDataForm>("formAutenticar");
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            //verificar se os campos do formulário estão corretos
            if (_dataForm.Validate())
            {
                try
                {
                    var model = (AutenticarRequestModel)_dataForm.DataObject;

                    var serviceHelper = new ServicesHelper();
                    var result = await serviceHelper.Post<AutenticarRequestModel, AutenticarResponseModel>("autenticar", model);

                    await App.Current.MainPage.DisplayAlert($"Olá, {result.Nome}", "Sua autenticação foi realizada com sucesso, seja bem vindo!", "OK");

                    //Gravar os dados na secure storage.
                    await SecureStorage.Default.SetAsync("auth_user", JsonConvert.SerializeObject(result));

                    //Navegação para Dashboard
                    await Shell.Current.GoToAsync("///Dashboard");
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Aviso", $"Erro: {ex.Message}", "OK");
                    throw;
                }

            }
        }
    }
}
