using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iab330.Models {
    public class RoomDataAccess {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public RoomDataAccess() {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Room>();
       
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

        public int DeleteRoom(Room RoomInstance) {
            var id = RoomInstance.Id;
            return database.Delete<Room>(id);
        }
    }
}
