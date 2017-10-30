using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iab330.Services {
    public class BoxDataAccess {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public BoxDataAccess() {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Box>();
        }

        public ObservableCollection<Box> GetAllBoxes() {
            ObservableCollection<Box> boxCollection = new ObservableCollection<Box>();

            try {
                boxCollection = _getAllBoxes();
            } catch (Exception ex) {
                throw ex;
                //Error handling code here
            }

            return boxCollection;
        }
        private ObservableCollection<Box> _getAllBoxes() {
            List<Box> boxList = new List<Box>();
            ObservableCollection<Box> boxCollection = new ObservableCollection<Box>();

            try {
                lock (collisionLock) {
                    //boxList = database.Query<Box>("SELECT * FROM [Box]");
                    boxList = database.GetAllWithChildren<Box>();
                }
                if (boxList != null && boxList.Count > 0) {
                    foreach (Box box in boxList) {
                        box.Room = database.Get<Room>(box.RoomId);
                        boxCollection.Add(box);
                    }
                }
            } catch (Exception ex) {
                throw ex;
                //Error handling
            }
            return boxCollection;
        }
        // Inserts a box object into database
        public bool InsertBox(Box box) {
            bool isSuccessful = false;
            try {
                lock (collisionLock) {
                    database.Insert(box);
                }
                isSuccessful = true;
            } catch (Exception ex) {
                throw ex;
                //handler
            }
            return isSuccessful;
        }
        // Returns a list of box objects based on search query
        public List<Box> SearchBox(string query) {
            List<Box> boxes = new List<Box>();
            try {
                lock (collisionLock) {
                    boxes = database.Query<Box>("SELECT * FROM [Box] where name LIKE ?", "%" + query + "%");
                }
            } catch (Exception ex) {

            }
            return boxes;
        }
        // Gets the box object based on ID
        public Box GetBox(int id) {
            Box box = new Box();
            try {
                lock (collisionLock) {
                    box = database.Query<Box>("SELECT * FROM [Box] where id = ?", id)[0];
                }
            } catch (Exception ex) {
                throw ex;
                //
            }
            return box;
        }

        // Gets box based on box name
        public Box GetBox(string name) {
            Box box = new Box();
            try {
                lock (collisionLock) {
                    box = database.Query<Box>("SELECT * FROM [Box] where Name = ?", name)[0];
                }
            } catch (Exception ex) {
                throw ex;
                //
            }
            return box;
        }
        // Updates box object
        public bool UpdateBox(Box box) {
            bool isSuccessful = false;
            try {
                lock (collisionLock) {
                    database.Update(box);
                }
                isSuccessful = true;
            } catch (Exception ex) {
                throw ex;
                //Handler
            }
            return isSuccessful;
        }

        //Deletes box object
        public bool DeleteBox(Box box) {
            bool isSuccessful = false;
            var id = box.Id;
            try {
                if (id != 0) {
                    lock (collisionLock) {
                        var itemToDelete = database.FindWithChildren<Box>(id);
                        database.Delete(itemToDelete, true);
                    }
                    isSuccessful = true;
                }
            } catch (Exception ex) {
                throw ex;
                //Exception
            }
            return isSuccessful;
        }

        public void EstablishForeignKey(Box box) {
            lock (collisionLock) {
                database.UpdateWithChildren(box);
            }
        }
    }
}
