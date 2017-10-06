
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330 {
    public interface IDatabaseConnection {
        SQLiteConnection DbConnection();
    }
}
