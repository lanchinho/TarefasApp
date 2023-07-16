using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using Syncfusion.Maui.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TarefasApp.Services.Helpers;
using TarefasApp.Services.Model.Responses;
using TarefasApp.Services.Models.Requests;
using TarefasApp.UI.Views;

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


                    var myType = _formCriarTarefa.DataObject.GetType();
                    var props = new List<PropertyInfo>(myType.GetProperties());

                    foreach (PropertyInfo prop in props)
                    {
                        object propValue = prop.GetValue(_formCriarTarefa.DataObject, null);
                        propValue = null;

                        prop.SetValue(_formCriarTarefa.DataObject, propValue, null);

                    }

                    model.Descricao = string.Empty;
                    model.DataFim = DateTime.Now;
                    model.DataInicio = DateTime.Now;
                    model.Nome = string.Empty;
                    model.Categoria = string.Empty;

                    _formCriarTarefa.DataObject = model;

                    _formCriarTarefa.UpdateEditor("formCriarTarefa");
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

    }
}
