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
            var roomList = App.RoomAccess.GetAllRooms();
            roomsView.ItemsSource = roomList;

        }

        private void addRoomButton_Clicked(object sender, EventArgs e) {
            if (App.RoomAccess.GetRoom(roomName.Text).Count == 0) {
                var newRoom = new Room {
                    Name = roomName.Text
                };

                App.RoomAccess.SaveRoom(newRoom);
                var roomList = App.RoomAccess.GetAllRooms();
                roomsView.ItemsSource = roomList;
            } else {
                var error = new Label();
                error.Text = "Room Already Exists";
                errors.Children.Add(error);
            }
        }
    }
}