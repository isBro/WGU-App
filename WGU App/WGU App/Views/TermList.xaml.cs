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
	public partial class TermList : ContentPage
	{
		public TermList ()
		{
			InitializeComponent ();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			TermCollectionView.ItemsSource = await DatabaseService.GetTerms();
		}

		private async void TermCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			var term = (Term)e.CurrentSelection.FirstOrDefault();
			if (e.CurrentSelection != null)
			{
				await Navigation.PushAsync(new EditTerm(term));
				
			}
		}

        private async void CourseList_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync (new CourseList());
        }
    }
}