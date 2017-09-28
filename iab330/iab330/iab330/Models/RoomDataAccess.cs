using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iab330.Models {
    public class RoomDataAccess {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public ObservableCollection<Room> Rooms { get; set; }

        public RoomDataAccess() {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Room>();
            this.Rooms = new ObservableCollection<Room>(database.Table<Room>());
        }

        //Observabelcollection methods 
        public void AddNewRoom(Room roomInstance) {
            this.Rooms.Add(roomInstance);
        }

        public IEnumerable<Room> GetObservableRoom(int id) {
            return this.Rooms.Where(room => room.Id == id);
        }


        public IEnumerable<Room> GetObservableRoom(string name) {
            return this.Rooms.Where(room => room.Name == name);
        }

        public List<Room> SearchRoom(string query) {
            lock (collisionLock) {
                return database.Query<Room>("SELECT * FROM [Room] where name = ?", "%" + query + "%");
            }
        }

        public List<Room> GetRoom(int id) {
            lock (collisionLock) {
                return database.Query<Room>("SELECT * FROM [Room] where id = ?", id);
            }
        }


        public List<Room> GetRoom(string name) {
            lock (collisionLock) {
                return database.Query<Room>("SELECT * FROM [Room] where Name = ?", name);
            }
        }

        public List<Room> GetAllRooms() {
            lock (collisionLock) {
                return database.Query<Room>("SELECT * FROM [Room]");
            }
        }

        public int EditRoom(Room RoomInstance) {
            return database.Update(RoomInstance);
        }


        public int SaveRoom(Room RoomInstance) {
            lock (collisionLock) {
                if (RoomInstance.Id != 0) {
                    return database.Update(RoomInstance);
                } else {
                    return database.Insert(RoomInstance);
                }
            }
        }

        public void SaveAllRooms() {
            lock (collisionLock) {
                foreach (var roomInstance in this.Rooms) {
                    if (roomInstance.Id != 0) {
                        database.Update(roomInstance);
                    } else {
                        database.Insert(roomInstance);
                    }
                }
            }
        }

        public int DeleteRoom(Room RoomInstance) {
            var id = RoomInstance.Id;
            if (id != 0) {
                lock (collisionLock) {
                    database.Delete<Room>(id);
                }
            }
            this.Rooms.Remove(RoomInstance);
            return id;
        }

    
        public void DeleteAllCustomers() {
            lock (collisionLock) {
                database.DropTable<Room>();
                database.CreateTable<Room>();
            }
            this.Rooms = null;
            this.Rooms = new ObservableCollection<Room>
              (database.Table<Room>());
        }
    }
}
