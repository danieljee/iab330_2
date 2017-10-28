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
    public partial class HomePage : ContentPage
    {
        // Initialize page 
        public HomePage()
        {
            InitializeComponent();
        }
        // Navigatie to manage box page
        private void manageBoxesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageBoxScreen());
        }
        // Navigate to search page
        private void searchButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPage());
        }
        //Navigate to help page
        private void helpButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpScreen());
        }
        // Navigate to add room page
        private void addRoomButton_Clicked(object sender, EventArgs e) {
            Navigation.PushAsync(new AddRoom());
        }
    }
}