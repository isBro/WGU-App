using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGU_App.Services;

namespace WGU_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddInstructor : ContentPage
    {
        public AddInstructor()
        {
            InitializeComponent();
        }

        private readonly int selectedCourseId;

        public AddInstructor(int id)
        {
            InitializeComponent();

            selectedCourseId = id;
            courseid.Text = selectedCourseId.ToString();




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

            await DatabaseService.AddCourseInstructor(selectedCourseId, instructorName.Text, instructorEmail.Text, instructorPhone.Text);
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
    }
}