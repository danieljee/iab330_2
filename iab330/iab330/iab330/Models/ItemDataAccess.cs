using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iab330.Models {
    public class ItemDataAccess {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ItemDataAccess() {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Item>();
            if (!database.Table<Item>().Any()) { //if table is empty

                var newItem1 = new Item() {
                    Name = "MySecondItem",
                    Quantity = 1,
                };
                App.ItemAccess.SaveItem(newItem1);

            }
        }

        public List<Item> GetItem(int id) {
            lock (collisionLock) {
                return database.Query<Item>("SELECT * FROM [Item] where id = id");
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

        public int DeleteItem(Item itemInstance) {
            var id = itemInstance.Id;
            return database.Delete<Item>(id);
        }
    }
}
