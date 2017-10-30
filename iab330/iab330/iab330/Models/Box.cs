using iab330.Models;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330 {
    [Table("Box")]
    // Sets model variables for Box
    public class Box {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey(typeof(Room))]
        public int RoomId { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Item> Items { get; set; }

        [ManyToOne]
        public Room Room { get; set; }
    }
}
