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
        private ItemDataAccess itemDataAccess;
        private BoxDataAccess boxDataAccess;
        private string _error;
        private string _newItemName;
        private string _newItemQuantity;
        private Box _selectedBox = null;

        public ItemViewModel() {
            itemDataAccess = DataAccessLocator.ItemDataAccess;
            boxDataAccess = DataAccessLocator.BoxDataAccess;
            Items = itemDataAccess.GetAllItems();
            //Set the selected box as the first box in the collection.
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
                    };
                    itemDataAccess.InsertItem(newItem);

                    if(SelectedBox.Items == null) {
                        SelectedBox.Items = new List<Item> { newItem };
                    } else {
                        SelectedBox.Items.Add(newItem);
                    }

                    boxDataAccess.EstablishForeignKey(SelectedBox);
                    Items.Add(newItem);
                    Error = "Item added!";

                    if (newItem.Box.Name == SelectedBox.Name) {
                        Error = "Foreign Key Established";
                    }
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
        }

        public ICommand CreateItemCommand { protected set; get; }
        public ICommand RemoveItemCommand { protected set; get; }

        public ObservableCollection<Item> Items {
            get { return _items; }
            set {
                if (_items != value) {
                    _items = value;
                    OnPropertyChanged("Itemes");
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
