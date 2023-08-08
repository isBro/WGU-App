using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WGU_App.Services;

namespace WGU_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DatabaseService.LoadSampleData();
        }

        public void Button_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Button Pressed!");
            DatabaseService.ClearSampleData();
            DisplayAlert("Button Pressed", "Button Pressed!", "OK");
        }

        private async void AddTerm_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Add Term Button Pressed!");
        }

        private async void ViewTerms_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("View Terms Button Pressed!");
            await Navigation.PushAsync(new TermList());
        }
    }
}