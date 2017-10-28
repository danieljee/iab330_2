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
    public partial class EditRoom : ContentPage {
        // Initialize page and bindings
        public EditRoom() {
            InitializeComponent();
            BindingContext = ViewModelLocator.RoomsViewModel;
        }
    }
}