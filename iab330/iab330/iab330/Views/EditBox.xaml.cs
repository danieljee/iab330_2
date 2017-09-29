using iab330.Models;
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
        static Box observableBox;
        static IEnumerable<Item> itemsInTheBox;
        public EditBox(Box targetBox) {
            InitializeComponent();
            observableBox = App.BoxDataAccess.GetObservableBox(targetBox.Id).First();
            this.BindingContext = observableBox;
            boxName.Placeholder = observableBox.Name;
            rooms.BindingContext = App.RoomDataAccess;
            itemsInTheBox = App.ItemDataAccess.GetObservableItemWithBoxId(targetBox.Id);
            items.ItemsSource = itemsInTheBox;
        }

        private void editBoxButton_Clicked(object sender, EventArgs e) {
            error.Text = "";
            if (String.IsNullOrEmpty(boxName.Text)) { //Make sure box name isn't empty
                error.Text = "Please enter the name";
                return;
            } else if (App.BoxDataAccess.GetBox(boxName.Text).Count > 0) { //Make sure the same named box doesn't exist
                error.Text = "Box name already used";
                return;
            } else if (rooms.SelectedIndex < 0) { //If room is not selected (using the initial room)
                observableBox.Name = boxName.Text;
                App.BoxDataAccess.SaveBox(observableBox);
                DisplayAlert("Success", "Edited Name: " + boxName.Text, "OK");
                foreach(var item in itemsInTheBox) { //Make sure each item's BoxName is updated as well.
                    item.BoxName = boxName.Text;
                    App.ItemDataAccess.SaveItem(item);
                }
           
                return;
            }
            var roomName = rooms.Items[rooms.SelectedIndex];
            observableBox.Name = boxName.Text;
            observableBox.RoomName = roomName;
            observableBox.RoomId = App.RoomDataAccess.GetRoom(roomName)[0].Id;


            App.BoxDataAccess.SaveBox(observableBox);
            foreach (var item in itemsInTheBox) {
                item.BoxName = boxName.Text;
                App.ItemDataAccess.SaveItem(item);
            }
            DisplayAlert("Success", "Edited Name: "+boxName.Text + "Room: " + roomName, "OK");
        }
    }
}