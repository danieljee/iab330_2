using iab330.Models;
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
            boxes.BindingContext = App.BoxDataAccess;
		}

        private void addItemButton_Clicked(object sender, EventArgs e) {
            error.Text = "";
            int number;
            if (String.IsNullOrEmpty(itemName.Text)) {
                error.Text = "Please enter the name of the item";
                return;
            } else if (boxes.SelectedIndex < 0) {
                error.Text = "Please Choose a box";
                return;
            } else if (!Int32.TryParse(quantity.Text, out number)) {
                error.Text = "Please enter a number for the quantity";
                return;
            }
            var boxName = boxes.Items[boxes.SelectedIndex];
            var selectedBox = App.BoxDataAccess.GetBox(boxName)[0];

            var newItem = new Item {
                Name = itemName.Text,
                Quantity = number,
                BoxId = selectedBox.Id,
                BoxName = boxName
            };
            App.ItemDataAccess.AddNewItem(newItem);
            App.ItemDataAccess.SaveItem(newItem);
            error.Text = "Item added successfully";
        }
    }
}