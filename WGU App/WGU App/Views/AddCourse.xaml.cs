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
	public partial class AddCourse : ContentPage
	{
		public AddCourse ()
		{
			InitializeComponent ();
		}


		public AddCourse(int termId)
		{
			InitializeComponent ();

			_selectedTermId = termId;

		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
			IsPassed.SelectedItem = false.ToString();
			IsPassed.Title = false.ToString();
        }

        private readonly int _selectedTermId;

        private async void SaveCourse_Clicked(object sender, EventArgs e)
        {

			if (String.IsNullOrWhiteSpace(CourseName.Text))
			{
				await DisplayAlert("Course name missing", "Please enter a course name", "OK");
				return;
			}

			if (String.IsNullOrWhiteSpace(CourseDescription.Text))
			{
                await DisplayAlert("Course description missing", "Please enter a course description", "OK");
				return;
            }

			if (string.IsNullOrWhiteSpace(CourseTitle.Text))
			{
                await DisplayAlert("Course title is missing", "please enter a course title", "OK");
				return;
            }

            if (CourseEnd.Date < CourseStart.Date)
            {
                await DisplayAlert("End date is wrong", $"End date for this course cannot be be before {CourseStart.Date}", "OK");
                return;
            }



            await DatabaseService.AddCourse(_selectedTermId, CourseName.Text, CourseTitle.Text, CourseDescription.Text, CourseStart.Date, CourseEnd.Date, CourseNotes.Text, Notification.IsToggled, bool.Parse(IsPassed.SelectedItem.ToString()));
			await Navigation.PopAsync();


        }

        private async void CancelCourse_Clicked(object sender, EventArgs e)
        {
			await DisplayAlert("Course Canceled", "Course Canceled", "OK");
			await Navigation.PopAsync();
        }

        private async void Home_Clicked(object sender, EventArgs e)
        {
			await Navigation.PopToRootAsync();
        }
    }
}