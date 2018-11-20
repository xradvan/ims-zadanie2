using System;
using Xamarin.Forms;

namespace zadanie2
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            masterPage.listView.ItemSelected += OnItemSelected;
        }

        /// <summary>
        /// Metoda obsluhuje udalost vybratia polozky z menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.listView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
