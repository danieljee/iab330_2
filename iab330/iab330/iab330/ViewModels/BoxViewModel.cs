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
        private string _error;
        private string _newBoxName;
        private Room _selectedRoom = null;

        public BoxViewModel() {
            boxDataAccess = DataAccessLocator.BoxDataAccess;
            Boxes = boxDataAccess.GetAllBoxes();
            //Set the selected room as the first room in the collection.
            CreateBoxCommand = new Command(
                () => {
                    Error = "";
                    if (SelectedRoom == null) {//If no room is selected
                        Error = "Please select a room";
                        return;
                    } else if (String.IsNullOrEmpty(NewBoxName)) { //If name field is empty. Not needed?
                        Error = "Please enter the box name";
                        return;
                    }
                    
                    var newBox = new Box {
                        Name = NewBoxName,
                        RoomId = SelectedRoom.Id,
                        RoomName = SelectedRoom.Name
                    };
                    Boxes.Add(newBox);
                    boxDataAccess.InsertBox(newBox);
                },
                () => {
                    return true;
                }
            );

            RemoveBoxCommand = new Command<Box>(
                (box) => {
                    Boxes.Remove(box);
                    boxDataAccess.DeleteBox(box);
                }
            );
        }

        public ICommand CreateBoxCommand { protected set; get; }
        public ICommand RemoveBoxCommand { protected set; get; }

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
    }
}
