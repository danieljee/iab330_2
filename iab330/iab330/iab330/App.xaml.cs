using iab330.Models;
using iab330.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace iab330 {
    public partial class App : Application {
        static ItemDataAccess itemDataAccess;

        public App() {
            InitializeComponent();
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

        public static void SetMainPage() {
            Current.MainPage = new HomePage {

            };
        }

        public void OnPause() {
            //roomDataAccess.SaveAllRooms(); //Make sure to save all data before pausing

        }
    }
}
