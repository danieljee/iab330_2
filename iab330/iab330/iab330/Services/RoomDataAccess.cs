using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace iab330 {
    public class RoomDataAccess {
        private SQLiteConnection database;
        private static object collisionLock = new object();

        public RoomDataAccess() {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
            database.CreateTable<Room>();
        }
        
        /* This is used to initialize the ObservableCollection property of RoomsViewModel.
         */
        public ObservableCollection<Room> GetAllRooms() {
            ObservableCollection<Room> roomCollection = new ObservableCollection<Room>();

            try {
                roomCollection = _getAllRooms();
                if (roomCollection == null || roomCollection.Count == 0) {
                    _populateRooms();
                    roomCollection = _getAllRooms();
                }
            } catch (Exception ex) {
                //Error handling code here
            }

            return roomCollection;
        }

        private ObservableCollection<Room> _getAllRooms() {
            List<Room> roomList = new List<Room>();
            ObservableCollection<Room> roomCollection = new ObservableCollection<Room>();

            try {
                lock (collisionLock) {
                    //roomList = database.Query<Room>("SELECT * FROM [Room]");
                    roomList = database.GetAllWithChildren<Room>();
                }
                if (roomList != null && roomList.Count > 0) {
                    foreach(Room room in roomList) {
                        roomCollection.Add(room);
                    }
                }
            } catch (Exception ex) {
                //Error handling
            }
            return roomCollection;
        }

        private void _populateRooms() {
            lock (collisionLock) {
                InsertRoom(new Room() { Name = "Kitchen" });
                InsertRoom(new Room() { Name = "Room 1" });
                InsertRoom(new Room() { Name = "Living Room" });
                InsertRoom(new Room() { Name = "Storage Space 1" });
            }
        }
        //C
        public bool InsertRoom(Room room) {
            bool isSuccessful = false;
            try {
                lock (collisionLock) {
                    database.Insert(room);
                }
                isSuccessful = true;
            } catch (Exception ex) {
                //handler
            }
            return isSuccessful;
        }

        //R
        public List<Room> SearchRoom(string query) {
            List<Room> rooms = new List<Room>();
            try {
                lock (collisionLock) {
                    rooms = database.Query<Room>("SELECT * FROM [Room] where name = ?", "%" + query + "%");
                }
            } catch (Exception ex) {

            }
            return rooms;
        }

        public Room GetRoom(int id) {
            Room room = new Room();
            try {
                lock (collisionLock) {
                    room = database.Query<Room>("SELECT * FROM [Room] where id = ?", id)[0];
                }
            } catch (Exception ex) {

            }
            return room;
        }


        public Room GetRoom(string name) {
            Room room = new Room();
            try {
                lock (collisionLock) {
                    room = database.Query<Room>("SELECT * FROM [Room] where Name = ?", name)[0];
                }
            } catch (Exception ex) {

            }
            return room;
        }
        
        //U
        public bool UpdateRoom(Room room) {
            bool isSuccessful = false;
            try {
                lock (collisionLock) {
                    database.Update(room);
                }
                isSuccessful = true;
            } catch (Exception ex) {
                //Handler
            }
            return isSuccessful;
        }

        //D
        public bool DeleteRoom(Room room) {
            bool isSuccessful = false;
            var id = room.Id;
            try {
                if (id != 0) {
                    lock (collisionLock) {
                        /*
                         * database.Delete recursively will delete objects that are already in memory
                         * It will not access database to load relationships of objects to delete.
                         * Therefore, FindWithChildren will be use to load all the related children to memory.
                         */ 
                        var itemToDelete = database.FindWithChildren<Room>(id, true);
                        database.Delete(itemToDelete, true);
                    }
                    isSuccessful = true;
                }
            } catch (Exception ex) {
                //Exception
            }
            return isSuccessful;
        }

        /* UpdateWithChildren makes sure that all the Boxes in the Boxes List
         * are referencing this particular room, without manually assigning it.
         */
        public void EstablishForeignKey(Room room) {
            lock (collisionLock) {
                database.UpdateWithChildren(room);
            }
        }
    }
}
