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
            roomType.BindingContext = App.RoomDataAccess;
            boxList.BindingContext = App.BoxDataAccess;
        }

        private void createBox_Clicked(object sender, EventArgs e) {
            errors.Children.Clear();
            if (roomType.SelectedIndex < 0) {//If no room is selected
                var newLabel = new Label();
                newLabel.Text = "Please choose a room";
                errors.Children.Add(newLabel);
                return;
            } else if (String.IsNullOrEmpty(boxName.Text)) { //If name field is empty
                System.Diagnostics.Debug.WriteLine("boxName empty");
                var newLabel = new Label();
                newLabel.Text = "Please enter the box name";
                errors.Children.Add(newLabel);
                return;
            } 
            var room = App.RoomDataAccess.GetRoom(roomType.Items[roomType.SelectedIndex])[0]; //Get the room from db.
            var newBox = new Box {
                Name = boxName.Text,
                RoomId = room.Id,
                RoomName = room.Name
            };
            App.BoxDataAccess.AddNewBox(newBox);
            App.BoxDataAccess.SaveBox(newBox);
      
        }

        private void boxList_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
            Box selectedBox = (Box)boxList.SelectedItem;
            Navigation.PushAsync(new EditBox(selectedBox));
        }
    }
}