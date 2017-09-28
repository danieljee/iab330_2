using Android.App;
using Android.Content.PM;
using Android.OS;
using CodeMill.VMFirstNav;

namespace iab330.Droid {
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            NavigationService.Instance.RegisterViewModels(typeof(App).Assembly);
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            
            LoadApplication(new App());
        }
    }
}