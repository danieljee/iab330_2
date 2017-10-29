using iab330.Interfaces;
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
    public class ItemViewModel: BaseViewModel {
        private ObservableCollection<Item> _items;
        private IEnumerable<Item> _searchResult;
        private ItemDataAccess itemDataAccess;
        private BoxDataAccess boxDataAccess;
        private RoomDataAccess roomDataAccess;
        private string _error;
        private string _newItemName;
        private string _newItemQuantity;
        private Box _selectedBox = null;
        private string _searchCriteria = "Item";
        private Item _itemToBeEdited;
        private string _searchQuery = "";

        //public ObservableCollection<Item> getItemsFromRooms(ObservableCollection<Room> rooms) {
        //    ObservableCollection<Item> items = new ObservableCollection<Item>();
        //    foreach (Room room in rooms) {
        //        foreach (Box box in room.Boxes) {
        //            foreach (Item item in box.Items) {
        //                items.Add(item);
        //            }
        //        }
        //    }
        //    return items;
        //}

        public ItemViewModel() {
            itemDataAccess = DataAccessLocator.ItemDataAccess;
            boxDataAccess = DataAccessLocator.BoxDataAccess;
            roomDataAccess = DataAccessLocator.RoomDataAccess;
            ItemsToBeEdited = itemDataAccess.GetAllItems();

            //Items = getItemsFromRooms(ViewModelLocator.RoomsViewModel.Rooms);

            CreateItemCommand = new Command(
                () => {
                    Error = "";
                    int quantity;
                    if (String.IsNullOrEmpty(NewItemName)) { //If name field is empty. Not needed?
                        Application.Current.MainPage.DisplayAlert("Error", "Please enter the item's name.", "Ok");
                        //Error = "Please enter the item name";
                        return;
                    } else if (!Int32.TryParse(NewItemQuantity, out quantity)) {
                        Application.Current.MainPage.DisplayAlert("Error", "Quantity should be a number.", "Ok");
                        //Error = "Quantity should be a number";
                        return;
                    } else if (quantity < 1) {
                        Application.Current.MainPage.DisplayAlert("Error", "Please enter a quantity larger than zero.", "Ok");
                        //Error = "Please enter a quantity larger than zero";
                        return;
                    } else if (SelectedBox == null) {//If no box is selected
                        Application.Current.MainPage.DisplayAlert("Error", "Please select a box.", "Ok");
                        //Error = "Please select a box";
                        return;
                    }
                        var newItem = new Item {
                        Name = NewItemName,
                        Quantity = quantity,
                        //BoxName = SelectedBox.Name
                    };
                    itemDataAccess.InsertItem(newItem);

                    if (SelectedBox.Items == null) {
                        SelectedBox.Items = new List<Item> { newItem };
                    } else {
                        SelectedBox.Items.Add(newItem);
                    }
                    boxDataAccess.EstablishForeignKey(SelectedBox);
                    ItemsToBeEdited.Add(newItem);

                    if (ViewModelLocator.BoxViewModel.BoxToBeEdited.Id == SelectedBox.Id) {
                        ViewModelLocator.BoxViewModel.BoxToBeEditedItems = ItemsToBeEdited;
                    }
                    Error = "Item added!";
                    SelectedBox = null;
                    NewItemName = "";
                    NewItemQuantity = "";
                },
                () => {
                    return true;
                }
            );

            RemoveItemCommand = new Command<Item>(
                async (item) =>
                {
                    bool answer = await Application.Current.MainPage.DisplayAlert("Delete Item", "Are you sure you want to delete item?", "Yes", "No");
                    if (answer)
                    {
                        ItemsToBeEdited.Remove(item);
                        itemDataAccess.DeleteItem(item);
                        SearchCommand.Execute(null);
                    }
                }
            );

            UpdateItemCommand = new Command(
                () => {
                    Int32 quantity;
                    if (!string.IsNullOrEmpty(NewItemName)) {
                        if (!string.IsNullOrEmpty(NewItemQuantity)) {
                            if (!Int32.TryParse(NewItemQuantity, out quantity)) {
                                Application.Current.MainPage.DisplayAlert("Error", "Quantity should be a number.", "Ok");
                                //Error = "Quantity should be number";
                                return;
                            }
                            ItemToBeEdited.Quantity = quantity;
                        }
                      
                        ItemToBeEdited.Name = NewItemName;
                        itemDataAccess.UpdateItem(ItemToBeEdited);
                    }

                    if (SelectedBox != null && (ItemToBeEdited.Box != SelectedBox)) {
                        if (SelectedBox.Items == null) {
                            SelectedBox.Items = new List<Item> { ItemToBeEdited };
                        } else {
                            SelectedBox.Items.Add(ItemToBeEdited);
                        }
                        boxDataAccess.EstablishForeignKey(SelectedBox);
                    }
                    ItemsToBeEdited = itemDataAccess.GetAllItems();
                    ViewModelLocator.BoxViewModel.Boxes = boxDataAccess.GetAllBoxes();
                    NewItemName = "";
                    NewItemQuantity = "";
                    SelectedBox = null;
                    Error = "Edited!";
                }
            );

            SearchCommand = new Command(
                () => {
                    if (_searchCriteria == "Item") {
                        SearchResult = ItemsToBeEdited.Where(item => item.Name.StartsWith(SearchQuery));
                    } else if (_searchCriteria == "Box") {
                        SearchResult = ItemsToBeEdited.Where(item => boxDataAccess.GetBox(item.BoxId).Name.StartsWith(SearchQuery));
                    } else {
                        SearchResult = ItemsToBeEdited.Where(item => roomDataAccess.GetRoom(boxDataAccess.GetBox(item.BoxId).RoomId).Name.StartsWith(SearchQuery));
                    }

                    foreach(var item in SearchResult) {
                        item.Box.Room = roomDataAccess.GetRoom(item.Box.RoomId);
                    }
                }
            );

            ExportDataCommand = new Command(
                () =>
                {
                    var fileService = DependencyService.Get<IDataExport>();
                    string itemList = "Room Type, Box Name, Item Name, Qty" + Environment.NewLine;
                    var range = _items.Count;
                    for (int index = 0; index < range; index++)
                    {
                        itemList += _items[index].Box.Room.Name + ", " + _items[index].Box.Name + ", " + _items[index].Name + ", " + _items[index].Quantity + ", " + Environment.NewLine;
                    }

                    fileService.ExportData(itemList);
                }
            );
        }

        public ICommand CreateItemCommand { protected set; get; }
        public ICommand RemoveItemCommand { protected set; get; }
        public ICommand UpdateItemCommand { protected set; get; }
        public ICommand SearchCommand { protected set; get; }
        public ICommand ExportDataCommand { protected set; get; }
        public Item ItemToBeEdited {
            get {
                return _itemToBeEdited;
            }
            set {
                if (_itemToBeEdited != value) {
                    _itemToBeEdited = value;
                    OnPropertyChanged("ItemToBeEdited");
                }
            }
        }

        public IEnumerable<Item> SearchResult {
            get {
              
                return _searchResult;
            }
            set {
                if (_searchResult != value) {
                    _searchResult = value;
                    OnPropertyChanged("SearchResult");
                }
            }
        }

        public string SearchQuery {
            get {
                return _searchQuery;
            }
            set {
                if (_searchQuery != value) {
                    _searchQuery = value;
                    OnPropertyChanged("SearchQuery");
                }
            }
        }

        public ObservableCollection<Item> ItemsToBeEdited {
            get { return _items; }
            set {
                if (_items != value) {
                    _items = value;
                    OnPropertyChanged("Items");
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

        public string NewItemName {
            get { return _newItemName; }
            set {
                if (_newItemName != value) {
                    _newItemName = value;
                    OnPropertyChanged("NewItemName");
                }
            }
        }

        public string NewItemQuantity {
            get { return _newItemQuantity; }
            set {
                if (_newItemQuantity != value) {
                    _newItemQuantity = value;
                    OnPropertyChanged("NewItemQuantity");
                }
            }
        }

        public Box SelectedBox {
            get {
                return _selectedBox;
            }
            set {
                if (_selectedBox != value) {
                    _selectedBox = value;
                    OnPropertyChanged("SelectedBox");
                }
            }
        }

        public string SelectedCriteria {
            get { return _searchCriteria; }
            set {
                if(_searchQuery != value) {
                    _searchCriteria = value;
                    OnPropertyChanged("SelectedCriteria");
                }
            }
        }
    }
}
