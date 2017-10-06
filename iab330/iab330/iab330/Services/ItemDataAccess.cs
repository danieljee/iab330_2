using iab330.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iab330.Services {
    public class ItemDataAccess {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ItemDataAccess() {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Item>();
        }



        public ObservableCollection<Item> GetAllItems() {
            ObservableCollection<Item> itemCollection = new ObservableCollection<Item>();

            try {
                itemCollection = _getAllItems();
            } catch (Exception ex) {
                throw ex;
                //Error handling code here
            }

            return itemCollection;
        }
        private ObservableCollection<Item> _getAllItems() {
            List<Item> itemList = new List<Item>();
            ObservableCollection<Item> itemCollection = new ObservableCollection<Item>();

            try {
                lock (collisionLock) {
                    itemList = database.Query<Item>("SELECT * FROM [Item]");
                }
                if (itemList != null && itemList.Count > 0) {
                    foreach (Item Item in itemList) {
                        itemCollection.Add(Item);
                    }
                }
            } catch (Exception ex) {
                throw ex;
                //Error handling
            }
            return itemCollection;
        }

        public bool InsertItem(Item item) {
            bool isSuccessful = false;
            try {
                lock (collisionLock) {
                    database.Insert(item);
                }
                isSuccessful = true;
            } catch (Exception ex) {
                throw ex;
                //handler
            }
            return isSuccessful;
        }

        public List<Item> SearchItem(string query) {
            List<Item> items = new List<Item>();
            try {
                lock (collisionLock) {
                    items = database.Query<Item>("SELECT * FROM [Item] where name LIKE ?", "%" + query + "%");
                }
            } catch (Exception ex) {

            }
            return items;
        }

        public Item GetItem(int id) {
            Item items = new Item();
            try {
                lock (collisionLock) {
                    items = database.Query<Item>("SELECT * FROM [Item] where id = ?", id)[0];
                }
            } catch (Exception ex) {
                throw ex;
                //
            }
            return items;
        }


        public Item GetItem(string name) {
            Item item = new Item();
            try {
                lock (collisionLock) {
                    item = database.Query<Item>("SELECT * FROM [Item] where Name = ?", name)[0];
                }
            } catch (Exception ex) {
                throw ex;
                //
            }
            return item;
        }

        public bool UpdateItem(Item item) {
            bool isSuccessful = false;
            try {
                lock (collisionLock) {
                    database.Update(item);
                }
                isSuccessful = true;
            } catch (Exception ex) {
                throw ex;
                //Handler
            }
            return isSuccessful;
        }

        //D
        public bool DeleteItem(Item item) {
            bool isSuccessful = false;
            var id = item.Id;
            try {
                if (id != 0) {
                    lock (collisionLock) {
                        database.Delete<Item>(id);
                    }
                    isSuccessful = true;
                }
            } catch (Exception ex) {
                throw ex;
                //Exception
            }
            return isSuccessful;
        }

    }
}
