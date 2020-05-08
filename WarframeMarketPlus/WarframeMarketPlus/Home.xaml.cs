using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warframe;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WarframeMarketPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {

        private App monApp = (Application.Current as App);

        public Home()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Attention", "Vous semblez ne pas être connecté à internet !", "OK");
            }
            string test = await WMGetter.Test();
            if (string.IsNullOrEmpty(test))
            {
                await DisplayAlert("Erreur", "Erreur dans la récupération des données", "OK");
            }
        }

        private async void butOffres_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(monApp.PageOffres);
        }
    }
}