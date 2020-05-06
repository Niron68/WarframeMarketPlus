using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarframeMarketPlus.ViewModel;
using Warframe;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace WarframeMarketPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupReliquePage : Rg.Plugins.Popup.Pages.PopupPage
    {

        public ViewRelique relique;

        public PopupReliquePage(ViewRelique _relique)
        {
            InitializeComponent();
            relique = _relique;
            foreach (KeyValuePair<Item, Rarity> item in relique.Loot)
            {
                StackLayout stack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };
                stack.Children.Add(new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start,
                    Text = item.Value.ToString()
                });
                stack.Children.Add(new Label
                {
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Text = item.Key.Name,
                    Margin = new Thickness(15, 5)
                });
                stack.Children.Add(new Label
                {
                    HorizontalOptions = LayoutOptions.End,
                    Text = item.Key.Price
                });
                listItem.Children.Add(stack);
            }
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += QuitPopup;
            mainStack.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private async void QuitPopup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}