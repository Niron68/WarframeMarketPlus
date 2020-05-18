using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WarframeMarketPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParameterPage : ContentPage
    {

        private App monApp = Application.Current as App;

        public ParameterPage()
        {
            InitializeComponent();
        }

        private async void butBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void butEffacerDonnees_Clicked(object sender, EventArgs e)
        {
            await monApp.DepotItems.DeleteAllItem();
            await DisplayAlert("Done", "All items have been deleted", "OK");
        }
    }
}