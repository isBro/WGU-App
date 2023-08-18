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
	public partial class CourseList : ContentPage
	{
		public CourseList ()
		{
			InitializeComponent ();


		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            {
                var courses = await DatabaseService.GetCourses();
                CourseCollectionView.ItemsSource = courses;
            }
        }

        private async void CourseCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var course = (Course)e.CurrentSelection.FirstOrDefault();

            if (course != null)
            {
                await Navigation.PushAsync(new EditCourse(course));
            }
        }
    }
}