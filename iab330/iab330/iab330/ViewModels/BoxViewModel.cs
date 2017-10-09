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
    public class BoxViewModel: BaseViewModel {
        private ObservableCollection<Box> _boxes;
        private BoxDataAccess boxDataAccess;
        private RoomDataAccess roomDataAccess;
        private ItemDataAccess itemDataAccess;
        private string _error;
        private string _newBoxName;
        private Room _selectedRoom = null;

        private Box _boxToBeEdited;

        /*
         * At the moment, this is the only way to get a collection of boxes with Room property referencing rooms.
         * When an element is loaded into memory either through database.get method or query, 
         * A property that needs to reference another model is set to null (no recursive retrival).
         * RoomDataAccess's getAllRooms function retrieves all rooms by using GetAllWithChildren which will fill Boxes property
         * Each box in Boxes property will have Room property set. 
         */
        //private ObservableCollection<Box> getBoxesFromRooms(ObservableCollection<Room> rooms) {
        //    ObservableCollection<Box> boxes = new ObservableCollection<Box>();
        //    foreach (Room room in rooms) {
        //        foreach (Box box in room.Boxes) {
        //            boxes.Add(box);
        //        }
        //    }
        //    return boxes;
        //}

        public BoxViewModel() {
            boxDataAccess = DataAccessLocator.BoxDataAccess;
            roomDataAccess = DataAccessLocator.RoomDataAccess;
            itemDataAccess = DataAccessLocator.ItemDataAccess;
            Boxes = boxDataAccess.GetAllBoxes();
            //Boxes = getBoxesFromRooms(ViewModelLocator.RoomsViewModel.Rooms);
            CreateBoxCommand = new Command(
                () => {
                    Error = "";
                    if (SelectedRoom == null) {//If no room is selected
                        
                        Error = "Please select a room";
                        return;
                    } else if (String.IsNullOrEmpty(NewBoxName)) { //If name field is empty. Not needed?
                        Error = "Please enter the box name";
                        return;
                    } else if (false) {
                        //Check to see if the box name is already being used
                    }
                    
                    var newBox = new Box {
                        Name = NewBoxName,
                        //RoomName = SelectedRoom.Name
                    };
                    boxDataAccess.InsertBox(newBox);

                    if (SelectedRoom.Boxes == null) {
                        SelectedRoom.Boxes = new List<Box> { newBox };
                    } else {
                        SelectedRoom.Boxes.Add(newBox);
                    }
                    roomDataAccess.EstablishForeignKey(SelectedRoom);
                    Boxes.Add(newBox);

                    NewBoxName = "";
                    SelectedRoom = null;
                },
                () => {
                    return true;
                }
            );

            RemoveBoxCommand = new Command<Box>(
                async (box) =>
                {
                    bool answer = await Application.Current.MainPage.DisplayAlert("Delete Box", "Are you sure you want to delete box?", "Yes", "No");
                    if (answer)
                    {
                        Boxes.Remove(box);
                        boxDataAccess.DeleteBox(box);
                        //May need to improve this
                        ViewModelLocator.ItemViewModel.Items = itemDataAccess.GetAllItems();
                    }
                }
            );

            UpdateBoxCommand = new Command(
                () => {
                    if (!string.IsNullOrEmpty(NewBoxName)) {
                        //Should I prevent duplicate box name?
                        BoxToBeEdited.Name = NewBoxName;
                        boxDataAccess.UpdateBox(BoxToBeEdited);
                    }

                    /*
                     * After changing the room, will deleting the previous room delete this box even if it has changed?
                     * test 1: Create a room, then a box, change the box's room then remove the room.
                     * test 2: Create a room then a box. Exit the app. Change the box's room then remove the room.
                     */
                    if (SelectedRoom != null && (BoxToBeEdited.Room != SelectedRoom)) {
                        if (SelectedRoom.Boxes == null) {
                            SelectedRoom.Boxes = new List<Box> { BoxToBeEdited };
                        } else {
                            SelectedRoom.Boxes.Add(BoxToBeEdited);
                        }
                        roomDataAccess.EstablishForeignKey(SelectedRoom);
                    }
                    SelectedRoom = null;
                    NewBoxName = "";
                    Error = "Edited!";
                    Boxes = boxDataAccess.GetAllBoxes();
                    ViewModelLocator.ItemViewModel.Items = itemDataAccess.GetAllItems();
                }
            );
        }

        public ICommand CreateBoxCommand { protected set; get; }
        public ICommand RemoveBoxCommand { protected set; get; }
        public ICommand UpdateBoxCommand { protected set; get; }

        public ObservableCollection<Box> Boxes {
            get { return _boxes; }
            set {
                if (_boxes != value) {
                    _boxes = value;
                    OnPropertyChanged("Boxes");
                }
            }
        }

        public string Error {
            get { return _error; }
            set {
                if (_error != value) {
                    _error = value;
                    OnPropertyChanged("Error");
                }
            }
        }

        public string NewBoxName {
            get { return _newBoxName; }
            set {
                if (_newBoxName != value) {
                    _newBoxName = value;
                    OnPropertyChanged("NewBoxName");
                }
            }
        }

        public Room SelectedRoom {
            get {
                return _selectedRoom;
            }
            set {
                if (_selectedRoom != value) {
                    _selectedRoom = value;
                    OnPropertyChanged("SelectedRoom");
                }
            }
        }

        public Box BoxToBeEdited {
            get {
                return _boxToBeEdited;
            }
            set {
                if (_boxToBeEdited != value) {
                    _boxToBeEdited = value;
                    OnPropertyChanged("BoxToBeEdited");
                }
            }
        }


    }
}
