using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iab330.Models {
    public class ItemDataAccess {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<Item> Items { get; set; }
        public ItemDataAccess() {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Item>();
            this.Items = new ObservableCollection<Item>(database.Table<Item>());
        }

        public void AddNewItem(Item itemInstance) {
            this.Items.Add(itemInstance);
        }

        public IEnumerable<Item> GetObservableItem(int id) {
            return this.Items.Where(item => item.Id == id);
        }


        public IEnumerable<Item> GetObservableItem(string name) {
            return this.Items.Where(item => item.Name == name);
        }

        public List<Item> searchItem(string query) {
            lock (collisionLock) {
                return database.Query<Item>("SELECT * FROM [Item] where name LIKE ?", "%"+query+"%");
            }
        }

        public List<Item> GetItem(int id) {
            lock (collisionLock) {
                return database.Query<Item>("SELECT * FROM [Item] where id = ?", id);
            }
        }

        public List<Item> GetItem(string name) {
            lock (collisionLock) {
                return database.Query<Item>("SELECT * FROM [Item] where name = ?", name);
            }
        }

        public List<Item> GetAllItems() {
            lock (collisionLock) {
                return database.Query<Item>("SELECT * FROM [Item]");
            }
        }

        public int EditItem(Item itemInstance) {
            return database.Update(itemInstance);
        }


        public int SaveItem(Item itemInstance) {
            lock (collisionLock) {
                if (itemInstance.Id != 0) {
                    return database.Update(itemInstance);
                } else {
                    return database.Insert(itemInstance);
                }
            }
        }
        public void SaveAllItems() {
            lock (collisionLock) {
                foreach (var itemInstance in this.Items) {
                    if (itemInstance.Id != 0) {
                        database.Update(itemInstance);
                    } else {
                        database.Insert(itemInstance);
                    }
                }
            }
        }

        public int DeleteItem(Item itemInstance) {
            var id = itemInstance.Id;
            if (id != 0) {
                lock (collisionLock) {
                    return database.Delete<Item>(id);
                }
            }
            this.Items.Remove(itemInstance);
            return id;
        }

        public void DeleteAllItem() {
            lock (collisionLock) {
                database.DropTable<Item>();
                database.CreateTable<Item>();
            }
            this.Items = null;
            this.Items = new ObservableCollection<Item>(database.Table<Item>());
        }

    }
}
