using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warframe;
using WarframeMarketPlus.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WarframeMarketPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReliquePage : ContentPage
    {

        public ViewDepotReliques DepotReliques
        {
            get
            {
                return (Application.Current as App).DepotReliques;
            }
        }

        public ReliquePage()
        {
            InitializeComponent();
        }

        private void entSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void butRefreshPrice_Clicked(object sender, EventArgs e)
        {

        }

        private async void listReliques_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewRelique viewRelique = listReliques.SelectedItem as ViewRelique;
            await PopupNavigation.Instance.PushAsync(new PopupReliquePage(viewRelique));
        }
    }
}