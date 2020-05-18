using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Warframe;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WarframeMarketPlus.ViewModel
{
    public class ViewDepotItems : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DepotItems _depotItems;

        private bool finished = true;

        private string _filter;

        private ObservableCollection<ViewItem> _allItems;

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
                               _allItems.Add(new ViewItem(i));
                           }
                       });
                        finished = true;
                    });
                }
                return _items;
            }
            private set
            {
                if(value != _items)
                {
                    _items = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Filter { get => _filter;
            set
            {
                if(value != _filter)
                {
                    _filter = value;
                    NotifyPropertyChanged();
                    Search();
                }
            }
        }

        public ViewDepotItems(string path)
        {
            _depotItems = new DepotItems(path);
            _items = new ObservableCollection<ViewItem>();
            _allItems = new ObservableCollection<ViewItem>();
        }

        public async Task DeleteAllItem()
        {
            await _depotItems.DeleteAllItem();
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

        public async void Search()
        {
            await SearchAsync();
        }

        private Task SearchAsync()
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(_filter))
                {
                    Items = _allItems;
                }
                else
                {
                    var temp =
                        (from it in _allItems
                         where it.Name.ToLower().Contains(_filter.ToLower())
                         select it).ToList();
                    Items = new ObservableCollection<ViewItem>(temp);
                }
            });
        }
    }
}
