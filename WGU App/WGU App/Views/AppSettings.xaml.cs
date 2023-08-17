using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGU_App.Services;

namespace WGU_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppSettings : ContentPage
	{
		public AppSettings ()
		{
			InitializeComponent ();
		}

        private void LoadSampleData_Clicked(object sender, EventArgs e)
        {
            DatabaseService.LoadSampleData();
        }

        private void ClearSampleData_Clicked(object sender, EventArgs e)
        {
            DatabaseService.ClearSampleData();
        }
    }
}