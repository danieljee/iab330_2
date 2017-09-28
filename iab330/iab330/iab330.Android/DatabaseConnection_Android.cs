using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(iab330.Droid.DatabaseConnection_Android))]
namespace iab330.Droid {
    public class DatabaseConnection_Android: IDatabaseConnection {
        public SQLiteConnection DbConnection() {
            var dbName = "InventoryDb.db3";
            var path = Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                    dbName
                );
            if (!File.Exists(path)) File.Create(path);
            return new SQLiteConnection(path);
        }
    }
}

//var dbName = "InventoryDb.db3";
//var path = Path.Combine(
//        System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
//        dbName
//    );
//            if (!File.Exists(path)) File.Create(path);
//            return new SQLiteConnection(new SQLitePlatformAndroid(),path);