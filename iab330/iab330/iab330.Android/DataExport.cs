using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using iab330.Interfaces;
using iab330.Droid;
using System.IO;

[assembly: Dependency(typeof(DataExport))]
namespace iab330.Droid
{
    public class DataExport : IDataExport
    {
        // Generates a list of items text file and displays alert when completed
        public async Task ExportData(string itemList)
        {
            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "tableOfItems.txt");
            File.WriteAllText(filename, itemList);

            var readText = File.ReadAllText(filename);
            await Application.Current.MainPage.DisplayAlert("Data Export", "Data successfully exported", "Ok");

        }
    }
}