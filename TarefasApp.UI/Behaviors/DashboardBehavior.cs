using Newtonsoft.Json;
using TarefasApp.Services.Model.Responses;

namespace TarefasApp.UI.Behaviors
{
    /// <summary>
    /// Classe para implementar os comportamentos da página
    /// </summary>
    public class DashboardBehavior : Behavior<ContentPage>
    {
        //atributos
        private AutenticarResponseModel _autenticarResponseModel;

        private Label _nomeUsuario;
        private Label _emailUsuario;
        private Button _btnLogout;

        /// <summary>
        /// Método para capturar os elementos da página
        /// e adicionar comportamentos e eventos
        /// </summary>
        protected override async void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            #region Capturar as informações do usuário autenticado

            var auth = await SecureStorage.GetAsync("auth_user");
            if (auth != null)
            {
                _autenticarResponseModel = JsonConvert.DeserializeObject<AutenticarResponseModel>(auth);

                #endregion

                #region Capturar os elementos da página

                _nomeUsuario = bindable.FindByName<Label>("nomeUsuario");
                _emailUsuario = bindable.FindByName<Label>("emailUsuario");
                _btnLogout = bindable.FindByName<Button>("btnLogout");

                #endregion

                #region Criando os eventos

                _nomeUsuario.Text = _autenticarResponseModel.Nome;
                _emailUsuario.Text = _autenticarResponseModel.Email;
                _btnLogout.Clicked += OnLogoutClicked;

                #endregion
            }
        }

        /// <summary>
        /// Função para processar o evento Clicked no botão de logout
        /// </summary>
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            var isAccepted = await App.Current.MainPage
                .DisplayAlert("Sair da Conta",
                              $"Olá {_autenticarResponseModel.Nome}, deseja realmente sair da sua conta?",
                              "Sim, sair da conta!", "Cancelar");

            if (isAccepted)
            {
                //apagar os dados gravados na Secure Storage
                SecureStorage.Default.Remove("auth_user");

                //redirecionar de volta para a MainPage
                await Shell.Current.GoToAsync("///MainPage");
            }
        }
    }
}



