using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iab330.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HelpScreen : ContentPage
	{
		public HelpScreen ()
		{
			InitializeComponent ();

            var helpText = new Label { Text = "" };

            var assembly = typeof(HelpScreen).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("iab330.help.txt");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            helpText.Text = text;

            Content = new StackLayout
            {
                Padding = new Thickness(20, 20, 20, 20),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                     new Label { FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
                     FontAttributes = FontAttributes.Bold }, helpText 
                }
            };
        }
	}
}