using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Warframe;
using Xamarin.Forms;

namespace WarframeMarketPlus.ViewModel
{
    public class ViewDepotReliques : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DepotReliques _depotReliques;

        private bool finished = true;

        private string _filter;

        private bool _ducats;

        private List<ViewRelique> _allReliques;

        private ObservableCollection<ViewRelique> _reliques;

        public ObservableCollection<ViewRelique> Reliques
        {
            get
            {
                if (_allReliques.Count == 0 && finished)
                {
                    finished = false;
                    Task.Run(async () =>
                    {
                        var allReliques = await _depotReliques.AllReliques();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            foreach (var i in allReliques)
                            {
                                _reliques.Add(new ViewRelique(i));
                                _allReliques.Add(new ViewRelique(i));
                            }
                        });
                        finished = true;
                    });
                }
                return _reliques;
            }
            private set
            {
                if(value != _reliques)
                {
                    _reliques = value;
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
        
        public bool Ducats { get => _ducats;
            set
            {
                if(value != _ducats)
                {
                    _ducats = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand FilterCommand { get; private set; }

        public ViewDepotReliques(ViewDepotItems viewDepotItems)
        {
            _depotReliques = new DepotReliques(viewDepotItems._depotItems);
            _reliques = new ObservableCollection<ViewRelique>();
            _allReliques = new List<ViewRelique>();
            FilterCommand = new Command(Search);
            _ducats = false;
        }

        public ObservableCollection<ViewRelique> OrderByPlatinium(List<ViewRelique> list, bool ducats)
        {
            if (ducats)
            {
                return new ObservableCollection<ViewRelique>(list.OrderByDescending(i => i.AverageDucats));
            }
            else
            {
                return new ObservableCollection<ViewRelique>(list.OrderByDescending(i => i.AveragePrice));
            }
        }

        public async void Search()
        {
            await SearchAsync();
        }

        public async void SwitchDucat()
        {
            await SwitchDucatAsync();
        }

        private Task SearchAsync()
        {
            return Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(_filter))
                {
                    Reliques = OrderByPlatinium(_allReliques, _ducats);
                }
                else
                {
                    var temp =
                        (from it in _allReliques
                         where it.DisplayName.ToLower().Contains(_filter.ToLower())
                         select it).ToList();
                    Reliques = OrderByPlatinium(temp, _ducats);
                }
            });
        }

        private Task SwitchDucatAsync()
        {
            Reliques = OrderByPlatinium(_reliques.ToList(), _ducats);
            return Task.Run(() =>
            {
                foreach(ViewRelique relique in _allReliques)
                {
                    relique.Ducats = _ducats;
                }
            });
        }

    }
}
