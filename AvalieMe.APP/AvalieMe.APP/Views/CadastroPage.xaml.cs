using AvalieMe.APP.API;
using AvalieMe.APP.Models;
using Plugin.Connectivity;
using System;
using System.ComponentModel;
using System.Net.Mail;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AvalieMe.APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPage : ContentPage, INotifyPropertyChanged
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

        public CadastroPage()
        {
            InitializeComponent();

            IsLoading = false;
            IsVisible = true;
            BindingContext = this;
        }

        private void btnVoltar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            var email = txtEmail.Text?.ToLower();
            var nome = txtNome.Text;
            var senha = txtSenha.Text;
            var senhaConfirm = txtSenhaConfirm.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Atenção", "Preencha o email", "OK");
                return;
            }

            if (!EmailValido(email))
            {
                await DisplayAlert("Atenção", "Digite um email válido", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(nome))
            {
                await DisplayAlert("Atenção", "Preencha o nome", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(senha))
            {
                await DisplayAlert("Atenção", "Preencha a senha", "OK");
                return;
            }

            if (senha.Length < 6)
            {
                await DisplayAlert("Atenção", "A senha deve conter no mínimo 6 dígitos", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(senhaConfirm))
            {
                await DisplayAlert("Atenção", "Preencha confirmação de senha", "OK");
                return;
            }

            if (senhaConfirm.Length < 6)
            {
                await DisplayAlert("Atenção", "A confirmação de senha deve conter no mínimo 6 dígitos", "OK");
                return;
            }

            if (senha != senhaConfirm)
            {
                await DisplayAlert("Atenção", "As senhas não conferem", "OK");
                return;
            }

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Atenção", "Você não tem conexão com a internet no momento.", "OK");
                return;
            }

            var usuario = new Usuario()
            {
                Email = email,
                Nome = nome,
                Senha = senha
            };

            IsLoading = true;
            IsVisible = false;

            var usuarioSalvo = await Service.SalvarUsuario(usuario);

            IsLoading = false;
            IsVisible = true;

            if (!string.IsNullOrEmpty(usuarioSalvo.Message))
            {
                await DisplayAlert("Atenção", usuarioSalvo.Message, "OK");
                return;
            }

            await Navigation.PushAsync(new MainPage(usuarioSalvo));
        }

        private bool EmailValido(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}