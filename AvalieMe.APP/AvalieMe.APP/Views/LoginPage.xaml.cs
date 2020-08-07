using AvalieMe.APP.API;
using MonkeyCache.SQLite;
using Plugin.Connectivity;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AvalieMe.APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage, INotifyPropertyChanged
    {
        private bool isLoading { get; set; }
        private bool isVisible { get; set; }

        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
                RaisePropertyChanged("IsVisible");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginPage()
        {
            InitializeComponent();

            IsLoading = false;
            IsVisible = true;
            BindingContext = this;

            Barrel.Current.EmptyAll();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            var login = txtLogin.Text?.ToLower();
            var senha = txtSenha.Text;

            if (string.IsNullOrWhiteSpace(login))
            {
                await DisplayAlert("Atenção", "Preencha o login", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                await DisplayAlert("Atenção", "Preencha a senha", "OK");
                return;
            }

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Atenção", "Você não tem conexão com a internet no momento.", "OK");
                return;
            }

            IsLoading = true;
            IsVisible = false;

            var usuario = await Service.AutenticarUsuario(login, senha);

            IsLoading = false;
            IsVisible = true;

            if (usuario == null || !string.IsNullOrEmpty(usuario.Message))
            {
                await DisplayAlert("Atenção", "Usuário não encontrado", "OK");
                return;
            }

            Barrel.Current.Add("usuario", usuario, TimeSpan.FromHours(1));

            await Navigation.PushAsync(new MainPage(usuario));
        }

        private void btnCadastrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CadastroPage());
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}