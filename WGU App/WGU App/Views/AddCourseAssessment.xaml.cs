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
        public AddCourseAssessment(int courseId)
        {
            InitializeComponent();
            selectedCourseId = courseId;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            courseId.Text = selectedCourseId.ToString();
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

            await DatabaseService.AddCourseAssessment(AssessmentName.Text, AssessmentDescription.Text, assessmentType.SelectedItem.ToString(), int.Parse(courseId.Text), false);
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