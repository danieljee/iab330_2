using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iab330.Interfaces;
using iab330.Droid;
using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;

[assembly: Dependency(typeof(CreateLabel))]

namespace iab330.Droid
{
    public class CreateLabel : ICreateLabel
    {
        public async Task SaveFile(string boxName, string roomType, string items)
        {

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(documents, "label.txt");
            string input = boxName + Environment.NewLine + roomType + Environment.NewLine + items;
            File.WriteAllText(filename, input);

            var readText = File.ReadAllText(filename);
            bool answer = await Application.Current.MainPage.DisplayAlert("Would you like to print this label?", readText, "Yes", "No");
            if (answer)
            {
                return; //At the moment user can only view the label, cannot print label unless device is connected to printer.
            }
        }
    }
}