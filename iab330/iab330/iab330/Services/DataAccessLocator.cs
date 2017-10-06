using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330.Services {

    public class DataAccessLocator {
        private static RoomDataAccess _roomDataAccess = null;
        private static BoxDataAccess _boxDataAccess = null;
        //private static ItemDataAccess _itemDataAccess = null;

        public static RoomDataAccess RoomDataAccess {
            get {
                if (_roomDataAccess == null) {
                    _roomDataAccess = new RoomDataAccess();
                }
                return _roomDataAccess;
            }
        }

        public static BoxDataAccess BoxDataAccess {
            get {
                if (_boxDataAccess == null) {
                    _boxDataAccess = new BoxDataAccess();
                }
                return _boxDataAccess;
            }
        }
    }
}
