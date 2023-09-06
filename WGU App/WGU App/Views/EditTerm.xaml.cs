using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGU_App.Services;
using WGU_App.Models;

namespace WGU_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditTerm : ContentPage
	{
		public EditTerm ()
		{
			InitializeComponent ();

		}

        private readonly int _selectedTermId;
        public EditTerm(Term term)
        {
            InitializeComponent ();

            _selectedTermId = term.Id;
            TermId.Text = $"{_selectedTermId}";
            TermName.Text = term.TermName;
            StartDatePicker.Date = term.StartDate;
            EndDatePicker.Date = term.EndDate;

        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            int countCourses = await DatabaseService.GetCourseCountAsync(_selectedTermId);
            CountLabel.Text = countCourses.ToString();
            var courseList = await DatabaseService.GetCourses(_selectedTermId);
            CourseCollectionView.ItemsSource = courseList;

        }

        private async void SaveTerm_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TermName.Text))
            {
                await DisplayAlert("Missing Name", "Please enter a name.", "OK");
                return;
            }

            await DatabaseService.UpdateTerm(_selectedTermId, TermName.Text, StartDatePicker.Date, EndDatePicker.Date);

            await Navigation.PopAsync();

        }

        private async void CancelTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete term and related courses?", "Delete this term?", "Yes", "No");
            if (answer == true)
            {
                await DatabaseService.RemoveTerm(int.Parse(TermId.Text));
                await DisplayAlert("Term Deleted", "Term Deleted", "Ok");

            }
            else
            {
                await DisplayAlert("Delete Canceled", "Nothing Deleted", "OK");
            }

            await Navigation.PopAsync();
        }

        private async void AddCourseButton_Clicked(object sender, EventArgs e)
        {
            var termId = _selectedTermId;
            await Navigation.PushAsync(new AddCourse(termId));
        }

        private async void CourseCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var course = (Course)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {

                await Navigation.PushAsync(new EditCourse(course));
               
            }
        }
    }
}