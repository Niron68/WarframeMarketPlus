﻿using Rg.Plugins.Popup.Services;
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

        public bool Ducats = false;
        public ReliquePage()
        {
            this.BindingContext = DepotReliques;
            InitializeComponent();
            listReliques.ItemsSource = DepotReliques.Reliques;
        }

        //private void entSearch_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(entSearch.Text))
        //    {
        //        listReliques.ItemsSource = DepotReliques.OrderByPlatinium(DepotReliques.Filter(entSearch.Text), swDucats.IsToggled);
        //    }
        //    else
        //    {
        //        listReliques.ItemsSource = DepotReliques.Reliques;
        //    }
        //}

        private void butRefreshPrice_Clicked(object sender, EventArgs e)
        {

        }

        private async void listReliques_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewRelique viewRelique = listReliques.SelectedItem as ViewRelique;
            await PopupNavigation.Instance.PushAsync(new PopupReliquePage(viewRelique));
        }

        private void swDucats_Toggled(object sender, ToggledEventArgs e)
        {
            DepotReliques.SwitchDucat();
        }

        //private void swDucats_Toggled(object sender, ToggledEventArgs e)
        //{
        //    foreach(ViewRelique relique in DepotReliques.Reliques)
        //    {
        //        relique.Ducats = swDucats.IsToggled;
        //    }
        //    listReliques.ItemsSource = DepotReliques.OrderByPlatinium(DepotReliques.Filter(entSearch.Text), swDucats.IsToggled);
        //}
    }
}