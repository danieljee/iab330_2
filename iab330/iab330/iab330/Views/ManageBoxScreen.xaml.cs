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
            var roomList = App.RoomAccess.GetAllRooms();
            roomType.ItemsSource = roomList;
        }
    }
}