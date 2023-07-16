using Newtonsoft.Json;
using Syncfusion.Maui.DataForm;
using TarefasApp.Services.Helpers;
using TarefasApp.Services.Model.Responses;
using TarefasApp.Services.Models.Requests;

namespace TarefasApp.UI.Behaviors
{
    public class TarefasCadastroBehavior : Behavior<ContentPage>
    {
        private SfDataForm _formCriarTarefa;
        private Button _btnCriarTarefa;

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            _formCriarTarefa = bindable.FindByName<SfDataForm>("formCriarTarefa");
            _btnCriarTarefa = bindable.FindByName<Button>("btnCriarTarefa");

            _btnCriarTarefa.Clicked += OnBtnCriarTarefaClicked;
            _btnCriarTarefa.Released += LimparFormulario;
        }      

        private async void OnBtnCriarTarefaClicked(object sender, EventArgs e)
        {
            if (_formCriarTarefa.Validate())
            {
                try
                {
                    var model = (TarefasCadastroRequestModel)_formCriarTarefa.DataObject;

                    var auth = await SecureStorage.GetAsync("auth_user");
                    var user = JsonConvert.DeserializeObject<AutenticarResponseModel>(auth);

                    var servicesHelper = new ServicesHelper(user.AccessToken);
                    var result = await servicesHelper.Post<TarefasCadastroRequestModel, TarefasConsultaResponseModel>("tarefas", model);

                    

                    await App.Current.MainPage.DisplayAlert("Sucesso!", $"Tarefa '{result.Nome}', cadastrada com sucesso.", "Ok");
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Erro!", ex.Message, "OK");
                    throw;
                }        
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Aviso!", "Ocorreram erros de validação no formulário, por favor verifique.", "OK");
            }
        }

        private async void LimparFormulario(object sender, EventArgs e)
        {
            await Task.Delay(1000);

            _formCriarTarefa.ValidationMode = DataFormValidationMode.Manual;
            
            (_formCriarTarefa.DataObject as TarefasCadastroRequestModel).Nome = string.Empty;
            _formCriarTarefa.UpdateEditor("Nome");

            (_formCriarTarefa.DataObject as TarefasCadastroRequestModel).Descricao = string.Empty;
            _formCriarTarefa.UpdateEditor("Descricao");

            (_formCriarTarefa.DataObject as TarefasCadastroRequestModel).Categoria = string.Empty;
            _formCriarTarefa.UpdateEditor("Categoria");

            (_formCriarTarefa.DataObject as TarefasCadastroRequestModel).DataInicio = DateTime.Now;
            _formCriarTarefa.UpdateEditor("DataInicio");

            (_formCriarTarefa.DataObject as TarefasCadastroRequestModel).DataFim = DateTime.Now;
            _formCriarTarefa.UpdateEditor("DataFim");

            _formCriarTarefa.ValidationMode = DataFormValidationMode.LostFocus;
        }
    }
}
