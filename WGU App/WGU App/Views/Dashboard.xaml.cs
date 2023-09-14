using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGU_App.Services;
using WGU_App.Models;
using Plugin.LocalNotifications;

namespace WGU_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        public string Alert = "No notifications";

        private async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTerm());
        }

        private async void ViewTerms_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("View Terms Button Pressed!");
            await Navigation.PushAsync(new TermList());
        }

        private async void AppSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppSettings());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var courseList = await DatabaseService.GetCourses();
            var courseAssessmentList = await DatabaseService.GetCourseAssessments();

            var notifyRandom = new Random();
            var notifyId = notifyRandom.Next(1000);

            foreach (Course courseRecord in courseList)
            {
                if (courseRecord.StartNotification == true)
                {
                    if (courseRecord.StartDate == DateTime.Today)
                    {

              
                        try
                        {
                            CrossLocalNotifications.Current.Show("Notice", $"{courseRecord.Name} begins today!", notifyId);
                        }
                        catch(Exception ex) { 

                           await DisplayAlert("", "EXCEPTION CAUGHT: " + ex.Message, "OK");
                        
                        }


                    }

                    else if (courseRecord.EndDate == DateTime.Today)
                    {
                        try
                        {
                            CrossLocalNotifications.Current.Show("Notice", $"{courseRecord.Name} ends today!", notifyId);
                        }
                        catch (Exception ex)
                        {

                            await DisplayAlert("", "EXCEPTION CAUGHT: " + ex.Message, "OK");

                        }
                    }
                }

                else
                {
                    alert.Text = "     No notifications";
                }
            }

            foreach (CourseAssessment courseAssessment in courseAssessmentList)
            {
                if (courseAssessment.AssessmentNotification)
                {
                    if (courseAssessment.DueDate == DateTime.Today)
                    {
                        try
                        {
                            CrossLocalNotifications.Current.Show("Notice", $"{courseAssessment.AssessmentName} is due today!", notifyId);
                        }
                        catch (Exception ex)
                        {

                            await DisplayAlert("", "EXCEPTION CAUGHT: " + ex.Message, "OK");

                        }
                    }
                }
            }
        }
    }
}