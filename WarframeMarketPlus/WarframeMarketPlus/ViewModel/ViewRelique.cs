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
