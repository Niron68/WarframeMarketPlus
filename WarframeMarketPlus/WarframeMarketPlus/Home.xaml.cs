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
    public partial class Home : ContentPage
    {

        private App monApp = (Application.Current as App);

        public Home()
        {
            InitializeComponent();
        }

        private async void butOffres_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(monApp.PageOffres);
        }
    }
}