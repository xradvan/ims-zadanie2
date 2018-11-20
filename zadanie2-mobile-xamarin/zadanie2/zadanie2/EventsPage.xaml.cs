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
	public partial class EventsPage : ContentPage
	{
        // Data z REST Api
        List<Termin> terminyList = new List<Termin>();
		public EventsPage ()
		{
            InitializeComponent ();
            
            // REST Api url
            var terminyUrl = @"https://ims-zadanie2-xradvan.azurewebsites.net/api/terminy";
            
            try
            {
                // Stiahnutie terminov
                Rest.Consume<Termin>(terminyUrl, ref terminyList);

                // Pridanie do ListView
                listView.ItemsSource = terminyList.Select(l => l.Nazov).ToList<string>();
                listView.ItemSelected += DisplayDetails;
            } 
            catch
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
            var terminToDislay = terminyList.Find(x => x.Nazov == item.ToString());
            DisplayAlert(terminToDislay.Nazov, "Dátum: " + terminToDislay.Datum + "\n\n" + terminToDislay.Text, "Ok");
        }
    }
}