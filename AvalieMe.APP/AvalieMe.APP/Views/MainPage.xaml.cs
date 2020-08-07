using AvalieMe.APP.Models;
using AvalieMe.APP.Views;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AvalieMe.APP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private bool comboVisible { get; set; }
        private bool btnVisible { get; set; }
        public Usuario Usuario { get; set; }

        public bool ComboVisible
        {
            get
            {
                return this.comboVisible;
            }
            set
            {
                this.comboVisible = value;
                RaisePropertyChanged("ComboVisible");
            }
        }

        public bool BtnVisible
        {
            get
            {
                return this.btnVisible;
            }
            set
            {
                this.btnVisible = value;
                RaisePropertyChanged("BtnVisible");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPage(Usuario usuario)
        {
            Usuario = usuario;

            InitializeComponent();

            ComboVisible = false;
            BtnVisible = true;
            BindingContext = this;
        }

        private void btnVotar_Clicked(object sender, System.EventArgs e)
        {
            BtnVisible = false;
            ComboVisible = true;
        }

        private void btnMeusTestes_Clicked(object sender, System.EventArgs e)
        {
            
        }

        private void btnVoltarOpcoes_Clicked(object sender, System.EventArgs e)
        {
            ComboVisible = false;
            BtnVisible = true;
        }

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private async Task btnBuscarTestes_ClickedAsync(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new VotarPage());
        }
    }
}
