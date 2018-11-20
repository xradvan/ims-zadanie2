using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace zadanie2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsPage : ContentPage
	{
        // Data z REST Api
        List<Novinka> novinkyList = new List<Novinka>();
        public NewsPage ()
		{
			InitializeComponent ();

            // REST Api url
            var novinkyUrl = @"https://ims-zadanie2-xradvan.azurewebsites.net/api/novinky";
           
            try
            {
                // Stiahnutie terminov
                Rest.Consume<Novinka>(novinkyUrl, ref novinkyList);
                // Pridanie do ListView
                listView.ItemsSource = novinkyList.Select(l => l.Nazov).ToList<string>();
                listView.ItemSelected += DisplayDetails;
                errorLabel.IsVisible = false;
            } catch
            {
                errorLabel.IsVisible = true;
                errorLabel.Text = "Nie ste pripojený na internet.";
            }
        }

        /// <summary>
        /// Zobrazenie podrobnosti o termine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayDetails(object sender, EventArgs e)
        {
            var item = listView.SelectedItem;
            var terminToDislay = novinkyList.Find(x => x.Nazov == item.ToString());
            DisplayAlert(terminToDislay.Nazov, "Dátum: " + terminToDislay.Datum + "\n\n" + terminToDislay.Text, "Ok");
        }
    }
}