using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGU_App.Models;
using WGU_App.Services;

namespace WGU_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTerm : ContentPage
	{
		public AddTerm ()
		{
			InitializeComponent ();
		}

        public AddTerm(Term term)
        {
            InitializeComponent ();
           
            

        }

        private async void SaveTerm_Clicked(object sender, EventArgs e)
        {
            decimal tossedDecimal;
            int tossedInt;

            //if (string.IsNullOrWhiteSpace(TermName.Text))
            //{
            //    await DisplayAlert("Missing Name", "Please enter a name.", "OK");
            //    return;
            //}

            //await DatabaseService.UpdateTerm(_selectedTermId, TermName.Text, StartDatePicker.Date, EndDatePicker.Date);

            //await Navigation.PopAsync();
        }

        private async void CancelTerm_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("", "", "");
            await Navigation.PopAsync();
        }
    }
}