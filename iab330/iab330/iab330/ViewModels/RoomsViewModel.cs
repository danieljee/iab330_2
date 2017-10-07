using iab330.Models;
using iab330.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace iab330.ViewModels {
    public class RoomsViewModel: BaseViewModel {
        private ObservableCollection<Room> _rooms;
        private RoomDataAccess roomDataAccess;
        private BoxDataAccess boxDataAccess;
        private ItemDataAccess itemDataAccess;
        private string _newRoomName = "", _error = "";

        public RoomsViewModel() {
            roomDataAccess = DataAccessLocator.RoomDataAccess;
            boxDataAccess = DataAccessLocator.BoxDataAccess;
            itemDataAccess = DataAccessLocator.ItemDataAccess;
            this.Rooms = roomDataAccess.GetAllRooms();
            /* Setup an add room command
             * The second lambda function determines if the button should be disabled (if returned false)
             */
            this.AddRoomCommand = new Command(
                () => {
                    this.Error = ""; //Always clear error
                    Room room = this.GetObservableRoom(this.NewRoomName);
                    if (String.IsNullOrEmpty(this.NewRoomName)) {
                        this.Error = "Please enter a name"; //This may be redundant
                    } else if (room == null) {
                        var newRoom = new Room {
                            Name = NewRoomName
                        };
                        this.Rooms.Add(newRoom); //Always add to ObservableCollection to update view
                        this.SaveRoom(newRoom);//Remove this later when OnPause save function is implemented
                        NewRoomName = ""; //Clear the input field
                    } else {
                        this.Error = "Room Already Exists";
                    }
                },
                () => {
                    return this.NewRoomName.Length > 0;
                }
            );

            this.RemoveRoomCommand = new Command<Room>(
                (room) => {
                    this.Rooms.Remove(room);
                    roomDataAccess.DeleteRoom(room);
                    /*
                     * Update Boxes ObservableCollection.
                     * Is there a better way to update the collection
                     * without reassigning it? 
                     */ 
                    ViewModelLocator.BoxViewModel.Boxes = boxDataAccess.GetAllBoxes();
                    ViewModelLocator.ItemViewModel.Items = itemDataAccess.GetAllItems();
                }
            );
        }

        public string Error {
            get {
                return _error;
            }
            set {
                if (_error != value) {
                    _error = value;
                    OnPropertyChanged("Error");
                }
            }
        }

        public string NewRoomName {
            get {
                return _newRoomName;
            }
            set {
                if (_newRoomName != value) {
                    _newRoomName = value;
                    OnPropertyChanged("NewRoomName");

                    //Check if the button should be disabled
                    ((Command)this.AddRoomCommand).ChangeCanExecute();
                }
            }
        }
        
        public ObservableCollection<Room> Rooms {
            get { return _rooms;  }
            set {
                if (_rooms != value) {
                    _rooms = value;
                    OnPropertyChanged("Rooms");
                }
            }
        }
        public Room GetObservableRoom(int id) {
            IEnumerable<Room> rooms = this.Rooms.Where(room => room.Id == id);
            if (!rooms.Any()) return null;
            return rooms.First();
        }

        public Room GetObservableRoom(string name) {
            IEnumerable<Room> rooms = this.Rooms.Where(room => room.Name == name);
            if (!rooms.Any()) return null;
            return rooms.First();
        }

        public ICommand AddRoomCommand { protected set; get; }
        public ICommand RemoveRoomCommand { protected set; get; }

        //This may be redundant
        public bool SaveRoom(Room room) {
            if (room.Id != 0) {
                return roomDataAccess.UpdateRoom(room);
            } else {
                return roomDataAccess.InsertRoom(room);
            }
        }

        public bool SaveAllRooms() {
            foreach (var room in this.Rooms) {
                if (room.Id != 0) {
                    roomDataAccess.UpdateRoom(room);
                } else {
                    roomDataAccess.InsertRoom(room);
                }
            }
            return true;
        }
    }
}
