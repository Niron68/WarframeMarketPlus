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
        public TabbedPage PageOffres { get; set; }

        public App()
        {
            DepotReliques = new ViewDepotReliques(DepotItems);
            InitializeComponent();

            PageOffres = new TabbedPage();
            PageOffres.Children.Add(new MainPage());
            PageOffres.Children.Add(new ReliquePage());
            MainPage = new Home();
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
