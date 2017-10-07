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
	public partial class AddRoom : ContentPage
	{
        private RoomsViewModel roomsViewModel; 
		public AddRoom ()
		{
			InitializeComponent ();
            roomsViewModel = ViewModelLocator.RoomsViewModel;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            BindingContext = roomsViewModel;
        }

        private void roomsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            Navigation.PushAsync(new EditRoom());
        }
    }
}