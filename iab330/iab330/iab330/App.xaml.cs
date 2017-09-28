using iab330.Models;
using iab330.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace iab330 {
    public partial class App : Application {

        static RoomDataAccess roomDataAccess;
        static BoxDataAccess boxDataAccess;
        static ItemDataAccess itemDataAccess;

        public App() {
            InitializeComponent();
            itemDataAccess = new ItemDataAccess();
            itemDataAccess.DeleteAllItem();
            this.MainPage = new NavigationPage(new HomePage())
            {
                BarBackgroundColor = Color.DarkGray,
                BarTextColor = Color.White,
            };
        }

        public static ItemDataAccess ItemDataAccess {
            get {
                if (itemDataAccess == null) {
                    itemDataAccess = new ItemDataAccess();
                }
                return itemDataAccess;
            }
        }

        public static BoxDataAccess BoxDataAccess {
            get {
                if (boxDataAccess == null) {
                    boxDataAccess = new BoxDataAccess();
                }
                return boxDataAccess;
            }
        }

        public static RoomDataAccess RoomDataAccess {
            get {
                if (roomDataAccess == null) {
                    roomDataAccess = new RoomDataAccess();
                }
                return roomDataAccess;
            }
        }

        public static void SetMainPage() {
            Current.MainPage = new HomePage {

            };
        }

        public void OnPause() {
            //roomDataAccess.SaveAllRooms(); //Make sure to save all data before pausing

        }
    }
}
