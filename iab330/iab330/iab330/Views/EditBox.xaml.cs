using iab330.Interfaces;
using iab330.Models;
using iab330.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iab330.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditBox : ContentPage {

        // Initialize page and bindings
        public EditBox() {
            InitializeComponent();
            EditBoxPage.BindingContext = ViewModelLocator.BoxViewModel;
            roomType.BindingContext = ViewModelLocator.RoomsViewModel;
        }
        // Button command to navigate to add item page
        private void addItemButton_Clicked(object sender, EventArgs e) {
            Navigation.PushAsync(new AddItem());
        }
    }
}