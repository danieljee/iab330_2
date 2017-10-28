using iab330.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iab330.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
		public SearchPage ()
		{
			InitializeComponent ();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            BindingContext = ViewModelLocator.ItemViewModel;
            searchButton.Command.Execute(null);
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            searchQuery.Text = "";
        }
        // Navigate to add item page
        private void AddButton_Clicked(object sender, EventArgs e) {
            Navigation.PushAsync(new AddItem());
        }

        // Navigate to selected item page
        private void SearchResult_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            Navigation.PushAsync(new EditItem());
        }
    }
}