using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Warframe;
using Xamarin.Forms;

namespace WarframeMarketPlus.ViewModel
{
    public class ViewDepotItems
    {
        public DepotItems _depotItems;

        private bool finished = true;

        private ObservableCollection<ViewItem> _items;
        public ObservableCollection<ViewItem> Items
        {
            get
            {
                if (_items.Count == 0 && finished)
                {
                    finished = false;
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
                        finished = true;
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
            await Task.Run(() =>
            {
                List<Task> tasks = new List<Task>();
                foreach(var item in _items)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        await item.RefreshPrice();
                        await _depotItems.RefreshItem(item.Item);
                    }));
                }
                Task.WaitAll(tasks.ToArray());
            });
        }

        public ObservableCollection<ViewItem> Order(ObservableCollection<ViewItem> list)
        {
            return new ObservableCollection<ViewItem>(list.OrderByDescending(i => i.MinPrice));
        }

        public ObservableCollection<ViewItem> Filter(string text)
        {
            var temp =
                (from it in Items
                 where it.Name.ToLower().Contains(text.ToLower())
                 select it).ToList();
            return new ObservableCollection<ViewItem>(temp);
        }
    }
}
