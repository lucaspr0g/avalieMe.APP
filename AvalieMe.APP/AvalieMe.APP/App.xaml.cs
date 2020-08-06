using AvalieMe.APP.Views;
using MonkeyCache.SQLite;
using Xamarin.Forms;

namespace AvalieMe.APP
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if DEBUG
            HotReloader.Current.Run(this);
#endif

            Barrel.ApplicationId = "avalieMe";

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
