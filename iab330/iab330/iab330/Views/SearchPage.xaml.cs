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
            searchResult.BindingContext = App.ItemDataAccess;
           
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            searchCriteria.SelectedIndex = 2;
        }



        private void AddButton_Clicked(object sender, EventArgs e) {
            Navigation.PushAsync(new AddItem());
        }

        private void SearchButton_Clicked(object sender, EventArgs e) {
            var query = searchQuery.Text;
             
            var criteria = searchCriteria.Items[searchCriteria.SelectedIndex];
            switch (criteria) {
                case "Room":
                   
                    break;
                case "Box":
                    break;
                case "Item":
                    searchResult.ItemsSource = App.ItemDataAccess.searchItem(query);
                    break;
                default:
                    break;
            }
        }
    }
}