using System;
using WGU_App.Services;
using WGU_App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WGU_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Settings.FirstRun)
            {
                DatabaseService.LoadSampleData();
                Settings.FirstRun = false;
            }

            var dashboard = new Dashboard();

            var navPage = new NavigationPage(dashboard);

            MainPage = navPage;
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
