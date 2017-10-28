using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330.ViewModels {
    /*Singleton class
     *Allows us to access only one instance of RoomsViewModel.
     */
    public static class ViewModelLocator {
        private static RoomsViewModel _roomsViewModel = null;
        private static BoxViewModel _boxViewModel = null;
        private static ItemViewModel _itemViewModel = null;
        public static RoomsViewModel RoomsViewModel {
            get {
                if (_roomsViewModel == null) {
                    _roomsViewModel = new RoomsViewModel();
                }
                _roomsViewModel.Error = "";
                return _roomsViewModel;
            }
        }

        public static BoxViewModel BoxViewModel {
            get {
                if (_boxViewModel == null) {
                    _boxViewModel = new BoxViewModel();
                }
                _boxViewModel.Error = "";
                return _boxViewModel;
            }
        }

        public static ItemViewModel ItemViewModel {
            get {
                if (_itemViewModel == null) {
                    _itemViewModel = new ItemViewModel();
                }
                _itemViewModel.Error = "";
                return _itemViewModel;
            }
        }
    }
}
