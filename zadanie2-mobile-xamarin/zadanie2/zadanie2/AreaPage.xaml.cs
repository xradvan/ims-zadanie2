using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace zadanie2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AreaPage : ContentPage
	{
        // List pre aplikacne oblasti
        List<ApplicationArea> appAreas = new List<ApplicationArea>();
        // URL adresa stranky
        string baseUrl = "http://www.automobilova-mechatronika.fei.stuba.sk/webstranka/";
        // Nastavnie farieb
        Color headingColor1 = Color.FromHex("#3498db");
        Color headingColor2 = Color.FromHex("#27ae60");
        Color textColor = Color.FromHex("#383434");

        public AreaPage ()
		{
			InitializeComponent ();

            try
            {
                // Nacitanie aplikacnych oblasti
                DownloadApplicationAreas(baseUrl + "?q=node/59/", ref appAreas);

                // Pridanie aplikacnych oblasti do picker-a
                foreach (var area in appAreas)
                {
                    areaPicker.Items.Add(area.Text);
                }
              
                errorLabel.IsVisible = false;
            }
            catch
            {
                // Error handling
                errorLabel.IsVisible = true;
                areaPicker.IsVisible = false;
                errorLabel.Text = "Nie ste pripojený na internet.";
            }
            
		}

        /// <summary>
        /// Ziskanie aplikacnych oblasti
        /// </summary>
        /// <param name="url">Url adresa stranky</param>
        /// <param name="list">List, do ktoreho sa nahraju aplikacne oblasti</param>
        void DownloadApplicationAreas(string url, ref List<ApplicationArea> list)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var areas = doc.DocumentNode.SelectNodes("//ul[contains(@class, 'list-4')]//li//a");

            foreach (var item in areas)
            {
                string href = item.Attributes["href"].Value;

                if (href.Length > 2)
                {
                    list.Add(new ApplicationArea
                    {
                        Url = href,
                        Text = item.InnerText
                    });
                }
            }
        }

        /// <summary>
        /// Funkcia sluzi ako handler na zmenu udalosti v t.j. vyber konkretnej aplikacnej oblasti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            // Vycistenie stacklayotu
            areaStack.Children.Clear();
            spansStack.Children.Clear();

            // Npi
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            AddPageContent(baseUrl + appAreas.ElementAt(selectedIndex).Url, baseUrl);
        }

        /// <summary>
        /// Funkcia vytvori label s pozadovanymi vlastnostami
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="margin"></param>
        /// <param name="fontSize"></param>
        /// <returns></returns>
        public Label CreateLabel(string text, Color color, Thickness margin, double fontSize)
        {
            return new Label {
                Text = text,
                Margin = margin,
                TextColor = color,
                FontSize = fontSize
            };
        }

        /// <summary>
        /// Funkcia prida obsah aplikacnej oblasti do stacklayoutu
        /// </summary>
        /// <param name="url"></param>
        /// <param name="baseUrl"></param>
        public void AddPageContent(string url, string baseUrl)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            
            // Parsovanie spanov
            var spans = doc.DocumentNode.SelectNodes("//div[contains(@class, 'content-block')]");
            foreach (var item in spans)
            {
                var childNodes = item.ChildNodes; 
                var h1 = CreateLabel(childNodes[1].InnerText, headingColor1, new Thickness(10, 5, 10, 5), 18);
                spansStack.Children.Add(h1);
                var text = CreateLabel(childNodes[3].InnerText, textColor, new Thickness(10, 0, 30, 0), 16);
                spansStack.Children.Add(text);
            }

            // Parsovanie obsahu aplikacnej oblasti
            var content = doc.DocumentNode.SelectNodes("//div[contains(@class, 'col-md-6')]");
            foreach (var item in content)
            {
                var childNodes = item.ChildNodes;
                // Parosvanie a vytvorenie obrazku
                if (childNodes[1].OriginalName == "img")
                {
                    var imgUrl = childNodes[1].Attributes["src"].Value.Substring(12);
                    var webImage = new Image { Source = ImageSource.FromUri(new Uri(baseUrl + imgUrl)) };
                    webImage.Margin = new Thickness(30, 10, 30, 10);
                    areaStack.Children.Add(webImage);

                }
                // Parsovananie a vytvaranie odstavcov a nadpisov
                else if (childNodes[1].OriginalName == "h3")
                {
                    var h3 = childNodes[1].InnerHtml;
                    var h = CreateLabel(h3, headingColor2, new Thickness(10, 5, 10, 5), 20);
                    areaStack.Children.Add(h);
                    foreach (var p in childNodes)
                    {
                        if (p.OriginalName == "p")
                        {
                            Label text = CreateLabel(p.InnerText, textColor, new Thickness(10, 0, 30, 0), 16);
                            areaStack.Children.Add(text);
                        }
                    }
                }
            }
        }
    }
}