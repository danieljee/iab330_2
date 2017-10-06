
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330.Models {
    [Table("Item")]
    public class Item {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(50)]
        public string Name { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(typeof(Box))]
        public int BoxId { get; set; }

        [ManyToOne]
        public Box Box { get; set; }
    }
}
