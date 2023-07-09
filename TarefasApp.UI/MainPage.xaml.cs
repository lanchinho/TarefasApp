using TarefasApp.UI.Views;

namespace TarefasApp.UI;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void EsqueciMinhaSenhaTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PasswordRecover());
    }

    public async void CriarContaTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register());
    }
}

