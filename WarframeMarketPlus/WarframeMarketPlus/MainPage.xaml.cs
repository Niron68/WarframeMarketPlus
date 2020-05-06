using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using WarframeMarketPlus.ViewModel;
using Xamarin.Forms;
using Warframe;

namespace WarframeMarketPlus
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        App monApp = Application.Current as App;
        public ViewDepotItems DepotItems
        {
            get
            {
                return (Application.Current as App).DepotItems;
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Attention", "Vous semblez ne pas être connecté à internet !", "OK");
            }
            string test = await WMGetter.Test();
            if (string.IsNullOrEmpty(test))
            {
                await DisplayAlert("Erreur", "Erreur dans la récupération des données", "OK");
            }
        }

        private async void butRefreshPrice_Clicked(object sender, EventArgs e)
        {
            //DepotItems depotItems = new DepotItems(FileSystem.AppDataDirectory);
            //List<Item> item = new List<Item>(await depotItems.AllItems());
            //await DisplayAlert("Info", DepotItems.Items.Count.ToString(), "OK");
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                butRefreshPrice.IsEnabled = false;
                await DepotItems.RefreshAllPrice();
                butRefreshPrice.IsEnabled = true;
            }
            else
            {
                await DisplayAlert("Attention", "Vous semblez ne pas être connecté à internet !", "OK");
            }
        }

        private void entSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(entSearch.Text))
            {
                listItems.ItemsSource = DepotItems.Order(DepotItems.Filter(entSearch.Text));
            }
            else
            {
                listItems.ItemsSource = DepotItems.Items;
            }
        }
    }
}
