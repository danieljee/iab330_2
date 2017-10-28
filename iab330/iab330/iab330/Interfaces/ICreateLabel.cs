using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330.Interfaces
{
    public interface ICreateLabel
    {
        Task SaveFile(string boxName, string roomType, string items);
    }
}
