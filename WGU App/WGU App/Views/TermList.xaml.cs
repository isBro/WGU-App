using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WGU_App.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TermList : ContentPage
	{
		public TermList ()
		{
			InitializeComponent ();
		}

        private void TermCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}