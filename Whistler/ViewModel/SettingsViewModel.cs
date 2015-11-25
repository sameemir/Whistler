using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistler.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        public string Limit { get; set; }
        public string Radius { get; set; }
        public string Interval { get; set; }

        public SettingsViewModel()
        {
            Limit = "Limit";
            Radius = "Radius in Miles";
            Interval = "Refresh Intervals in secs";
            this.RaisePropertyChanged();
        }

    }
}
