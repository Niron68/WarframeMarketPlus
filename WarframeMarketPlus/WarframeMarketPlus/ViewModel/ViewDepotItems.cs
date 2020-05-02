using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Warframe;
using Xamarin.Forms;

namespace WarframeMarketPlus.ViewModel
{
    public class ViewDepotItems
    {
        public DepotItems _depotItems;

        private ObservableCollection<ViewItem> _items;
        public ObservableCollection<ViewItem> Items
        {
            get
            {
                if (_items.Count == 0)
                {
                    Task.Run(async () =>
                    {
                        var allItems = await _depotItems.AllItems();
                        Device.BeginInvokeOnMainThread(() =>
                       {
                           foreach (var i in allItems)
                           {
                               _items.Add(new ViewItem(i));
                           }
                       });
                    });
                }
                return _items;
            }
        }

        public ViewDepotItems(string path)
        {
            _depotItems = new DepotItems(path);
            _items = new ObservableCollection<ViewItem>();
        }

        public async void AddItem(ViewItem item)
        {
            if (await _depotItems.AddItem(item.Item))
                _items.Add(item);
        }

        public async Task RefreshAllPrice()
        {
            foreach(var item in _items)
            {
                await item.RefreshPrice();
                await _depotItems.RefreshItem(item.Item);
            }
            
        }
    }
}
