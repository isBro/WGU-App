using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGU_App.Models;

namespace WGU_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditCourse : ContentPage
	{
		public EditCourse ()
		{
			InitializeComponent ();
		}

		public EditCourse(Course course)
		{
			InitializeComponent ();
		}
	}
}