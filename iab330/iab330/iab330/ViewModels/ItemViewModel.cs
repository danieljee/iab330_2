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
        private string _error;
        private string _newItemName;
        private string _newItemQuantity;
        private Box _selectedBox = null;
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
            Items = itemDataAccess.GetAllItems();
            //Items = getItemsFromRooms(ViewModelLocator.RoomsViewModel.Rooms);

            CreateItemCommand = new Command(
                () => {
                    Error = "";
                    int quantity;
                    if (SelectedBox == null) {//If no box is selected
                        Error = "Please select a box";
                        return;
                    } else if (String.IsNullOrEmpty(NewItemName)) { //If name field is empty. Not needed?
                        Error = "Please enter the item name";
                        return;
                    } else if (!Int32.TryParse(NewItemQuantity, out quantity)) {
                        Error = "Quantity should be a number";
                        return;
                    } else if (quantity < 1) {
                        Error = "Please enter a quantity larger than zero";
                        return;
                    }
                  
                    var newItem = new Item {
                        Name = NewItemName,
                        Quantity = quantity,
                        //BoxName = SelectedBox.Name
                    };
                    itemDataAccess.InsertItem(newItem);

                    if(SelectedBox.Items == null) {
                        SelectedBox.Items = new List<Item> { newItem };
                    } else {
                        SelectedBox.Items.Add(newItem);
                    }

                    boxDataAccess.EstablishForeignKey(SelectedBox);
                    Items.Add(newItem);
                    ViewModelLocator.BoxViewModel.Boxes = boxDataAccess.GetAllBoxes();
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
                (item) => {
                    Items.Remove(item);
                    itemDataAccess.DeleteItem(item);
                }
            );

            UpdateItemCommand = new Command(
                () => {
                    Int32 quantity;
                    if (!string.IsNullOrEmpty(NewItemName)) {
                        if (!string.IsNullOrEmpty(NewItemQuantity)) {
                            if (!Int32.TryParse(NewItemQuantity, out quantity)) {
                                Error = "Quantity should be number";
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
                    Items = itemDataAccess.GetAllItems();
                    ViewModelLocator.BoxViewModel.Boxes = boxDataAccess.GetAllBoxes();
                    NewItemName = "";
                    NewItemQuantity = "";
                    SelectedBox = null;
                    Error = "Edited!";
                }
            );

            SearchCommand = new Command(
                () => {
                    SearchResult = Items.Where(item => item.Name.StartsWith(SearchQuery));

                }
            );
        }

        public ICommand CreateItemCommand { protected set; get; }
        public ICommand RemoveItemCommand { protected set; get; }
        public ICommand UpdateItemCommand { protected set; get; }
        public ICommand SearchCommand { protected set; get; }

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

        public ObservableCollection<Item> Items {
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

    }
}
