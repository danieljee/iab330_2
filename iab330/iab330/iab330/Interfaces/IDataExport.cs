using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iab330.Interfaces
{
    public interface IDataExport
    {
        Task ExportData(string itemlist);
    }
}
