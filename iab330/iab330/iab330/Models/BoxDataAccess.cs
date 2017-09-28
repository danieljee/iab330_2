using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SQLite;

namespace iab330 {
    public class BoxDataAccess {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        //expose data for xaml files to allow data binding.
        //ObservableCollection has built-in support for change notification
        //While doing crud operations, ObservableCollection can contain
        //both objects retrieved from the database and new objects added
        //via the UI that haven't been saved yet, or pending edits over 
        //existing objects
        //public ObservableCollection<Box> Boxes { get; set; }

        public BoxDataAccess() {
            //returns platform specific implementation of new SQLiteConnection(path)
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Box>();
            //this.Boxes = new ObservableCollection<Box>(database.Table<Box>());

            //if (!database.Table<Box>().Any()) { //if table is empty
            //    AddNewBox("First Box!");
            //}
        }

        //public void AddNewBox(string name) {
        //    this.Boxes.Add(
        //        new Box {
        //            Name = name
        //        });
        //}

        public List<Box> GetBox(int id) {
            lock (collisionLock) {
                return database.Query<Box>("SELECT * FROM [Box] where id = id");
            }
        }

        public List<Box> GetAllBoxes() {
            lock (collisionLock) {
                return database.Query<Box>("SELECT * FROM [Box]");
            }
        }

        public int EditBox(Box boxInstance) {
            return database.Update(boxInstance);
        }


        public int SaveBox(Box boxInstance) {
            lock (collisionLock) {
                if (boxInstance.Id != 0) {
                    return database.Update(boxInstance);
                } else {
                    return database.Insert(boxInstance);
                }
            }
        }

        //public void SaveAllBoxes() {
        //    lock (collisionLock) {
        //        foreach (var boxInstance in this.Boxes) {
        //            if (boxInstance.Id != 0) {
        //                database.Update(boxInstance);
        //            } else {
        //                database.Insert(boxInstance);
        //            }
        //        }
        //    }
        //}

        public int DeleteBox(Box boxInstance) {
            var id = boxInstance.Id;
            return database.Delete<Box>(id);
            //if (id != 0) {
            //    lock (collisionLock) {
            //        return database.Delete<Box>(id);
            //    }
            //}
            //this.Boxes.Remove(boxInstance);
            //return id;
        }

        public void DeleteAllBoxes() {
            lock (collisionLock) {
                database.DropTable<Box>();
                database.CreateTable<Box>();
            }
            //this.Boxes = null;
            //this.Boxes = new ObservableCollection<Box>(database.Table<Box>());
        }
    }
}
