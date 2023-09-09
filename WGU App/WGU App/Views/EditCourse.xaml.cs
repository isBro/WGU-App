using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGU_App.Models;
using WGU_App.Services;
using Xamarin.Essentials;

namespace WGU_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditCourse : ContentPage
	{
		public EditCourse ()
		{
			InitializeComponent ();
		}

        private readonly int selectedCourseId;
        private readonly int selectedTermId;

        public CourseInstructor courseInstructor;
        

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
                IsPassed.Title = selectedCourse.IsPassed.ToString();

                selectedCourseId = selectedCourse.Id;

            }

            catch (Exception ex)
            {
                Console.WriteLine (ex.ToString ());

            }


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var instructor = await DatabaseService.GetCourseInstructor(selectedCourseId);



            if (instructor.FirstOrDefault() != null)
            {
                InstructorsName.Text = $"{instructor.FirstOrDefault().InstructorName}";

            }

            else
            {
                InstructorsName.Text = "No instructor assigned";
            }

            var assessments = await DatabaseService.GetCourseAssessments(selectedCourseId);

            AssessmentCollectionView.ItemsSource = assessments;
                
                

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

            if (CourseEnd.Date < CourseStart.Date)
            {
                await DisplayAlert("End date is wrong", $"End date for this course cannot be be before {CourseStart.Date}", "OK");
                return;
            }


            try
            {
                await DatabaseService.UpdateCourse(selectedCourseId, CourseName.Text, CourseTitle.Text, CourseDescription.Text, CourseStart.Date, CourseEnd.Date, Notification.IsToggled, selectedTermId, bool.Parse(IsPassed.Title.ToString()));

            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", $"{ex.Message}", "OK");
            }



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

        private async void ShareButton_Clicked(object sender, EventArgs e)
        {
            var text = CourseName.Text + " /" + CourseDescription.Text;
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share Text"
            });
        }

        private async void ShareUri_Clicked(object sender, EventArgs e)
        {
            string uri = "https://learn.microsoft.com/en-us/xamarin/essentials/share?tabs=android";
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share web link"
            });
        }

        private async void AssessmentCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var assessment = (CourseAssessment)e.CurrentSelection.FirstOrDefault();
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new EditCourseAssessment(assessment));
            }

            
        }

        private async void addInstructor_Clicked(object sender, EventArgs e)
        {
            int instructorCount = await DatabaseService.GetInstructorCountAsync(selectedCourseId);

            if (instructorCount > 0)
            {

                var instructors = await DatabaseService.GetCourseInstructor(selectedCourseId);
                var instructor = instructors.FirstOrDefault();

                if (instructor != null)
                {
                    await Navigation.PushAsync(new EditInstructor(instructor));
                }
                
            }

            else
            {
                await Navigation.PushAsync(new AddInstructor(selectedCourseId));

            }

            
        }

        private async void addAssessment_Clicked(object sender, EventArgs e)
        {
            var assessmentCount = await DatabaseService.GetAssessmentCountAsync(selectedCourseId);


            if (assessmentCount > 1)
            {
                await DisplayAlert("Cannot add assessment", "You can only have two assessments per course.", "OK");
                return;

            }

            else
            {
                await Navigation.PushAsync(new AddCourseAssessment(selectedCourseId));

            }


           
        }
    }
}