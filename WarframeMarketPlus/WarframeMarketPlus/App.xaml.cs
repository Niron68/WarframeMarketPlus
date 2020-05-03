using System;
using Warframe;
using WarframeMarketPlus.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WarframeMarketPlus
{
    public partial class App : Application
    {

        public ViewDepotItems DepotItems { get; } = new ViewDepotItems(FileSystem.AppDataDirectory);

        public App()
        {
            InitializeComponent();

            MainPage mainPage = new MainPage();
            MainPage = mainPage;
            mainPage.Nomble = DepotItems.Items.Count;
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
