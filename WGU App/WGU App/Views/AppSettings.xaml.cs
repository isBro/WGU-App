using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGU_App.Services;
using Xamarin.Essentials;

namespace WGU_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppSettings : ContentPage
	{
		public AppSettings ()
		{
			InitializeComponent ();
		}

        private async void LoadSampleData_Clicked(object sender, EventArgs e)
        {
            if (Settings.FirstRun)
            {
                DatabaseService.LoadSampleData();
                Settings.FirstRun = false;
                await Navigation.PopToRootAsync();
            }

            

        }

        private void ClearSampleData_Clicked(object sender, EventArgs e)
        {
            DatabaseService.ClearSampleData();
            Settings.FirstRun = true;
        }

        private void ClearPreferences_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
        }
    }
}