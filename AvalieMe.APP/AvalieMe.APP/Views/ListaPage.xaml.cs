using AvalieMe.APP.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AvalieMe.APP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPage : ContentPage
    {
        public Usuario Usuario { get; set; }

        public ListaPage(Usuario usuario)
        {
            Usuario = usuario;

            InitializeComponent();
        }
    }
}