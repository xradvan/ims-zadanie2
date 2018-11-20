using HtmlAgilityPack;
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
	public partial class ITPage : ContentPage
	{
        // Nastavenie farieb
        Color tableBckColor = Color.FromHex("#3498db");

        public ITPage ()
		{
			InitializeComponent ();
            CreateTable();
        }

        /// <summary>
        /// Parsovanie tabuliek odporucaneho studijneho planu 
        /// pre inziniersku informatiku
        /// </summary>
        public void CreateTable()
        {
            var web = new HtmlWeb();
            var doc = web.Load("http://www.automobilova-mechatronika.fei.stuba.sk/webstranka/?q=node/97/");
            var tables = doc.DocumentNode.SelectNodes("//div[contains(@class, 'table-responsive')]/table");

            // Nadpisy tabuliek
            string[] titles = new string[] { "1. Ročník ZS", "1.Ročník LS", "2.Ročník ZS", "2.Ročník LS" };

            var tablesec = 0;
            var firstRow = true;
            foreach (var table in tables)
            {
                // Parsovanie tabulky
                var ts = new TableSection(titles[tablesec++]);
                firstRow = true;
                foreach (var tbody in table.SelectNodes("tbody|thead"))
                {
                    foreach (var row in tbody.SelectNodes("tr"))
                    {
                        // Vytvorenie riadku
                        var vc = new ViewCell();
                        var sl = new StackLayout();
                        sl.Orientation = StackOrientation.Horizontal;

                        // Zvyraznenie zahlavia tabulky
                        if (firstRow)
                        {
                            sl.BackgroundColor = tableBckColor;
                            firstRow = false;
                        }
                        else
                        {
                            sl.BackgroundColor = Color.Azure;
                        }

                        foreach (var cell in row.SelectNodes("th|td"))
                        {
                            // Pridanie textu do tabulky
                            var l1 = new Label { Text = cell.InnerText, FontSize = 12 };
                            l1.WidthRequest = 90;
                            sl.Children.Add(l1);
                        }
                        // Pridanie noveho riadku do tabulky
                        vc.View = sl;
                        ts.Add(vc);
                    }
                }
                // Pridanie tabulky
                tableRoot.Add(ts);
            }
        }
    }
}