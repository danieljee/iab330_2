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
        public HomePage()
        {
            InitializeComponent();
        }

        private void manageBoxesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ManageBoxScreen());
        }

        private void addItemButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuickAddPage());
        }

        private void searchButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPage());
        }

        private void helpButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpScreen());
        }
    }
}