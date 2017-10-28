using iab330.Models;
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
	public partial class AddItem : ContentPage
	{
		public AddItem ()
		{
			InitializeComponent ();
            boxes.BindingContext = ViewModelLocator.BoxViewModel;
            AddItemPage.BindingContext = ViewModelLocator.ItemViewModel;

        }
    }
}