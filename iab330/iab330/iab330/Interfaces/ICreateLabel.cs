using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330.Interfaces
{
    public interface ICreateLabel
    {
        // Calls the code from iab330.droid
        Task SaveFile(string boxName, string roomType, string items);
    }
}
