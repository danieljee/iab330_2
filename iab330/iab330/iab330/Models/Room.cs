
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330 {
    [Table("Room")]
    public class Room : INotifyPropertyChanged {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id {
            get {
                return _id;
            }
            set {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _name;
        [NotNull, MaxLength(50)]
        public string Name {
            get {
                return _name;
            }
            set {
                this._name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
