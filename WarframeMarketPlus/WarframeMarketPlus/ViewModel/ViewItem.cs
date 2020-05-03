using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Warframe;

namespace WarframeMarketPlus.ViewModel
{
    public class ViewItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Item _item;
        public string Name
        {
            get
            {
                return _item.Name;
            }
            set
            {
                if(value != _item.Name)
                {
                    _item.Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Price
        {
            get
            {
                return _item.Price;
            }
        }

        public string MarketName
        {
            get
            {
                return _item.MarketName;
            }
        }

        public int MinPrice
        {
            get
            {
                return _item.MinPrice;
            }
            set
            {
                if (value != _item.MinPrice)
                {
                    _item.MinPrice = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(Price));
                }
            }
        }

        public Item Item
        {
            get
            {
                return _item;
            }
        }

        public ViewItem(Item item)
        {
            _item = item;
        }

        public async Task RefreshPrice()
        {
            MinPrice = await WMGetter.GetPrice(MarketName);
        }
    }
}
