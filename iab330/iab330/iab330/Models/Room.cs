
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330 {
    [Table("Room")]
    public class Room {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull, MaxLength(50)]
        public string Name { get; set; }

        /*
         * When a room is retrieved from the db, Boxes is set to null. 
         * Only when findWithChildren or getWithChildren method is called
         * sqlite extension will query database to retrieve all boxes referencing the room.
         */ 
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Box> Boxes { get; set; }
    }
}
