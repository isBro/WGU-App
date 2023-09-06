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
    public partial class EditCourseAssessment : ContentPage
    {
        public EditCourseAssessment()
        {
            InitializeComponent();
        }

        private readonly int selectedAssessmentId;
        private readonly int selectedCourseId;
        public EditCourseAssessment(CourseAssessment assessment)
        {

            InitializeComponent();

            assessmentName.Text = assessment.AssessmentName;
            assessmentDescription.Text = assessment.AssessmentDescription;
            assessmentType.SelectedItem = assessment.AssessmentType;
            //assessmentType.Title = assessment.AssessmentType;
            isPassed.SelectedItem = assessment.IsPassed.ToString();
            isPassed.Title = assessment.IsPassed.ToString();

            selectedAssessmentId = assessment.Id;
            selectedCourseId = assessment.CourseId;
           
            
        }

        private async void SaveAssessment_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(assessmentName.Text))
            {
                await DisplayAlert("Assessment name missing", "Please enter a name", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(assessmentDescription.Text))
            {
                await DisplayAlert("Assessment description missing", "Please enter a description", "OK");
                return;
            }

            await DatabaseService.UpdateCourseAssessment(selectedAssessmentId, assessmentName.Text, assessmentDescription.Text, assessmentType.SelectedItem.ToString(), selectedCourseId, bool.Parse(isPassed.SelectedItem.ToString()));
            await Navigation.PopAsync();
        }

        private async void CancelAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void DeleteAssessment_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Assessment description missing", "Please enter a description", "Yes", "No");
            
            if (answer)
            {
                await DatabaseService.RemoveCourseAssessment(selectedAssessmentId);
                await Navigation.PopAsync();
            }
            else
            {
                return;
            }


            await Navigation.PopAsync();

        }
    }
}