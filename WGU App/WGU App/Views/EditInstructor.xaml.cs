using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_App.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGU_App.Services;

namespace WGU_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditInstructor : ContentPage
    {
        public EditInstructor()
        {
            InitializeComponent();
        }

        private readonly int selectedCourseId;
        private readonly int selectedInstructorId;
        public EditInstructor(CourseInstructor instructor)
        {
            InitializeComponent();

        

            selectedCourseId = instructor.CourseId;
            selectedInstructorId = instructor.Id;

            courseid.Text = selectedCourseId.ToString();
            instructorName.Text = instructor.InstructorName;
            instructorEmail.Text = instructor.InstructorEmail;
            instructorPhone.Text = instructor.InstructorPhone;

        }

        private async void SaveInstructor_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(instructorName.Text))
            {
                await DisplayAlert("Please enter a name", "Please enter a name", "OK");
            }

            if (string.IsNullOrWhiteSpace(instructorEmail.Text))
            {
                await DisplayAlert("Please enter a email", "Please enter a email", "OK");
            }

            if (string.IsNullOrWhiteSpace(instructorPhone.Text))
            {
                await DisplayAlert("Please enter a phone", "Please enter a phone", "OK");
            }

            await DatabaseService.UpdateCourseInstructor(selectedInstructorId, instructorName.Text, instructorEmail.Text, instructorPhone.Text, selectedCourseId);
            await Navigation.PopAsync();
        }

        private async void CancelInstructor_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Are you sure?", "Are you sure you want to cancel?", "Yes", "No");

            if (answer)
            {
                await DisplayAlert("Cancelation successful", "", "OK");
                await Navigation.PopAsync();
            }
            return;
        }

        private async void DeleteInstructor_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Are you sure?", "Are you sure you want to delete?", "Yes", "No");

            if (answer)
            {
                await DatabaseService.RemoveCourseInstructor(selectedInstructorId);
                await DisplayAlert("Delete successful", "", "OK");
                await Navigation.PopAsync();
            }
            return;
        }
    }
}