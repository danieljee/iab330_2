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
    public partial class ManageBoxScreen : ContentPage
    {
        public ManageBoxScreen()
        {
            InitializeComponent();
        }


        protected override void OnAppearing() {
            base.OnAppearing();
            roomType.BindingContext = ViewModelLocator.RoomsViewModel;
            boxList.BindingContext = ViewModelLocator.BoxViewModel;
        }

        private void boxList_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            Navigation.PushAsync(new EditBox());
        }
    }
}