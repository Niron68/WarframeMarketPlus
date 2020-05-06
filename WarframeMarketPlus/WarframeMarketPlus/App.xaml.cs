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
        public ViewDepotReliques DepotReliques { get; }

        public App()
        {
            DepotReliques = new ViewDepotReliques(DepotItems);
            InitializeComponent();

            TabbedPage tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(new MainPage());
            tabbedPage.Children.Add(new ReliquePage());
            MainPage = tabbedPage;
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
