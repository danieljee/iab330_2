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

        public ObservableCollection<Box> Boxes { get; set; }

        public BoxDataAccess() {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Box>();
            this.Boxes = new ObservableCollection<Box>(database.Table<Box>());
        }

        public void AddNewBox(Box boxInstance) {
            this.Boxes.Add(boxInstance);
        }

        public IEnumerable<Box> GetObservableBox(int id) {
            return this.Boxes.Where(box => box.Id == id);
        }


        public IEnumerable<Box> GetObservableBox(string name) {
            return this.Boxes.Where(box => box.Name == name);
        }

        public List<Box> SearchBox(string query) {
            lock (collisionLock) {
                return database.Query<Box>("SELECT * FROM [Box] where name LIKE ?", "%" + query + "%");
            }
        }

        public List<Box> GetBox(int id) {
            lock (collisionLock) {
                return database.Query<Box>("SELECT * FROM [Box] where id = ?", id);
            }
        }

        public List<Box> GetBox(string name) {
            lock (collisionLock) {
                return database.Query<Box>("SELECT * FROM [Box] where name = ?", name);
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

        public void SaveAllBoxes() {
            lock (collisionLock) {
                foreach (var boxInstance in this.Boxes) {
                    if (boxInstance.Id != 0) {
                        database.Update(boxInstance);
                    } else {
                        database.Insert(boxInstance);
                    }
                }
            }
        }

        public int DeleteBox(Box boxInstance) {
            var id = boxInstance.Id;
            if (id != 0) {
                lock (collisionLock) {
                    return database.Delete<Box>(id);
                }
            }
            this.Boxes.Remove(boxInstance);
            return id;
        }

        public void DeleteAllBoxes() {
            lock (collisionLock) {
                database.DropTable<Box>();
                database.CreateTable<Box>();
            }
            this.Boxes = null;
            this.Boxes = new ObservableCollection<Box>(database.Table<Box>());
        }
    }
}
