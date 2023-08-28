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
	public partial class EditCourse : ContentPage
	{
		public EditCourse ()
		{
			InitializeComponent ();
		}

        private readonly int selectedTermId;
        private readonly CourseInstructor instructor;
        public EditCourse(Course selectedCourse)
		{
			InitializeComponent ();

            try
            {

                CourseId.Text = $"{selectedCourse.Id}";
                CourseName.Text = selectedCourse.Name;
                CourseTitle.Text = selectedCourse.Title;
                CourseDescription.Text = selectedCourse.Description;
                CourseStart.Date = selectedCourse.StartDate;
                CourseEnd.Date = selectedCourse.EndDate;
                Notification.IsToggled = selectedCourse.StartNotification;
                selectedTermId = selectedCourse.TermId;
                IsPassed.SelectedItem = selectedCourse.IsPassed.ToString();

            }

            catch (Exception ex)
            {
                Console.WriteLine (ex.ToString ());

            }

            


        }



        private async void SaveCourse_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CourseName.Text))
            {
                await DisplayAlert("Course name missing", "Please enter a course name", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CourseTitle.Text))
            {
                await DisplayAlert("Course title missing", "Please enter a course title", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(CourseDescription.Text))
            {
                await DisplayAlert("Course description missing", "Please enter a course description", "OK");
                return; 
            }

            await DatabaseService.UpdateCourse(int.Parse(CourseId.Text), CourseName.Text, CourseTitle.Text, CourseDescription.Text, CourseStart.Date, CourseEnd.Date, Notification.IsToggled, selectedTermId, bool.Parse(IsPassed.SelectedItem.ToString()));

            await Navigation.PopAsync();

        }

        private async void CancelCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete this course?", "Delete this course?", "Yes", "No");

            if (answer == true)
            {
                await DatabaseService.RemoveCourse(int.Parse(CourseId.Text));

                await DisplayAlert("Course deleted", "Course deleted", "OK");
                
                await Navigation.PopAsync();
            }

            else
            {
                await DisplayAlert("Delete canceled", "Nothing deleted", "OK");
               
            }

        }

        private void ShareButton_Clicked(object sender, EventArgs e)
        {

           

        }

        private void ShareUrl_Clicked(object sender, EventArgs e)
        {

        }

        private void AssessmentCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}