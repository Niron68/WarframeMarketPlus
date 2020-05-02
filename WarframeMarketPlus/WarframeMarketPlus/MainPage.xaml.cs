using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeMarketPlus.ViewModel;
using Xamarin.Forms;

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
            butRefreshPrice.IsEnabled = false;
            await DepotItems.RefreshAllPrice();
            butRefreshPrice.IsEnabled = true;
        }

        private async void butRefreshPrice_Clicked(object sender, EventArgs e)
        {
            butRefreshPrice.IsEnabled = false;
            await DepotItems.RefreshAllPrice();
            butRefreshPrice.IsEnabled = true;
        }
    }
}
