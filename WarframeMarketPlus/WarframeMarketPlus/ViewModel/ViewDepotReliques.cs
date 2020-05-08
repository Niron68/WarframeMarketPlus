using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warframe;
using Xamarin.Forms;

namespace WarframeMarketPlus.ViewModel
{
    public class ViewDepotReliques
    {
        public DepotReliques _depotReliques;

        private bool finished = true;

        private ObservableCollection<ViewRelique> _reliques;

        public ObservableCollection<ViewRelique> Reliques
        {
            get
            {
                if(_reliques.Count == 0 && finished)
                {
                    finished = false;
                    Task.Run(async () =>
                    {
                        var allReliques = await _depotReliques.AllReliques();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            foreach(var i in allReliques)
                            {
                                _reliques.Add(new ViewRelique(i));
                            }
                        });
                        finished = true;
                    });
                }
                return _reliques;
            }
            private set
            {
                _reliques = value;
            }
        }

        public ViewDepotReliques(ViewDepotItems viewDepotItems)
        {
            _depotReliques = new DepotReliques(viewDepotItems._depotItems);
            _reliques = new ObservableCollection<ViewRelique>();
        }

        public ObservableCollection<ViewRelique> OrderByPlatinium(bool ducats)
        {
            if (ducats)
            {
                return new ObservableCollection<ViewRelique>(Reliques.OrderByDescending(i => i.AverageDucats));
            }
            else
            {
                return new ObservableCollection<ViewRelique>(Reliques.OrderByDescending(i => i.AveragePrice));
            }
        }

    }
}
