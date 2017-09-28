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
		public AddRoom ()
		{
			InitializeComponent ();
            //var roomList = App.RoomAccess.GetAllRooms();
            //roomsView.ItemsSource = roomList;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            this.BindingContext = App.RoomDataAccess;
        }

        private void addRoomButton_Clicked(object sender, EventArgs e) {
            errors.Children.Clear();
            var rooms = App.RoomDataAccess.GetObservableRoom(roomName.Text);
            if (String.IsNullOrEmpty(roomName.Text)) {
                var error = new Label();
                error.Text = "Please enter a name";
                errors.Children.Add(error);
            } else if (!rooms.Any()) {
                var newRoom  = new Room {
                    Name = roomName.Text
                };

                App.RoomDataAccess.AddNewRoom(newRoom);
                App.RoomDataAccess.SaveRoom(newRoom);//Remove this later when OnPause save function is implemented
            } else {
                var error = new Label();
                error.Text = "Room Already Exists";
                errors.Children.Add(error);
            }
        }
    }
}