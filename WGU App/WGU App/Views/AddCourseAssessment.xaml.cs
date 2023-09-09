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
    public partial class AddCourseAssessment : ContentPage
    {
        public AddCourseAssessment()
        {
            InitializeComponent();
        }

        private readonly int selectedCourseId;
        private readonly string _PerfType = "Performance";
        private readonly string _ObjType = "Objective";
        public AddCourseAssessment(int courseId)
        {
            InitializeComponent();
            selectedCourseId = courseId;


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            courseId.Text = selectedCourseId.ToString();

            var Objcount = await DatabaseService.ObjAssessmentCountAsync(selectedCourseId);
            var PerfCount = await DatabaseService.PerfAssessmentCountAsync(selectedCourseId);

            if (Objcount > 0)
            {

                assessmentType.Title = _PerfType;
                assessmentType.SelectedItem = _PerfType;
                assessmentType.IsEnabled = false;
            }

            else if (PerfCount > 0)
            {
                assessmentType.Title = _ObjType;
                assessmentType.SelectedItem = _ObjType;
                assessmentType.IsEnabled = false;

            }
        }

        private async void SaveAssessment_Clicked(object sender, EventArgs e)
        {
            if ( string.IsNullOrWhiteSpace(AssessmentName.Text))
            {
                await DisplayAlert("Missing Name", "Please enter a name.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(AssessmentDescription.Text))
            {
                await DisplayAlert("Missing Description", "Please enter a description.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(courseId.Text))
            {
                await DisplayAlert("Missing course id", "Please enter an id.", "OK");
                return;
            }


            // ADD COURSE ASSESSMENT TO DB

            await DatabaseService.AddCourseAssessment(AssessmentName.Text, AssessmentDescription.Text, AssessmentDueDate.Date, assessmentType.SelectedItem.ToString(), int.Parse(courseId.Text), false);
            await Navigation.PopAsync();
        }

        private async void CancelAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Home_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}