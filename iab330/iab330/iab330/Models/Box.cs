using iab330.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330 {
    [Table("Box")]
    public class Box : INotifyPropertyChanged {
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

        //[OneToMany(CascadeOperations = CascadeOperation.All)]
        //public List<Item> Items { get; set; }

        //private int _roomId;
        //[]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
