using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Warframe;

namespace WarframeMarketPlus.ViewModel
{
    public class ViewRelique : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Relique _relique;

        public string Ere
        {
            get
            {
                return _relique.Ere.ToString();
            }
            set
            {
                if(value != _relique.Ere.ToString())
                {
                    if(Enum.TryParse<Era>(value, out Era era))
                    {
                        _relique.Ere = era;
                        NotifyPropertyChanged();
                    }
                }
            }
        }

        public string Name
        {
            get
            {
                return _relique.Name;
            }
            set
            {
                if (value != _relique.Name)
                {
                    _relique.Name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Dictionary<Item, Rarity> Loot
        {
            get
            {
                return _relique.Loot;
            }
            set
            {
                if (value != _relique.Loot)
                {
                    _relique.Loot = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool Ducats
        {
            get
            {
                return _relique.Ducats;
            }
            set
            {
                if(value != _relique.Ducats)
                {
                    _relique.Ducats = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged("Platinium");
                }
            }
        }

        public bool Platinium
        {
            get
            {
                return !Ducats;
            }
        }

        public string DisplayName
        {
            get
            {
                return Ere + " " + Name;
            }
        }

        public float AveragePrice
        {
            get
            {
                return _relique.AveragePrice;
            }
        }

        public float AverageDucats
        {
            get
            {
                return _relique.AverageDucats;
            }
        }

        public string DisplayDucats
        {
            get
            {
                return "Average Ducats : " + AverageDucats;
            }
        }

        public string DisplayPrice
        {
            get
            {
                return "Average Price : " + AveragePrice;
            }
        }

        public Relique Relique
        {
            get
            {
                return _relique;
            }
        }

        public ViewRelique(Relique relique)
        {
            _relique = relique;
        }
    }
}
