using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whistler.Helpers;
using Whistler.Model;
using Windows.UI.Xaml;

namespace Whistler.ViewModel
{
    public class WhistlerListViewModel : ViewModelBase
    {
        
        private WebServiceHandler webServiceHandler;
        private AnonyousWhistleModel anonyousWhistleModel;

        public WhistlerListViewModel()
        {
            webServiceHandler = new WebServiceHandler();
        }

        public AnonyousWhistleModel MatchingWhistles
        {
            get
            {
                return anonyousWhistleModel;
            }
            set
            {
                anonyousWhistleModel = value;
                this.RaisePropertyChanged();
            }
        }

        //Progress bar
        private bool _IsActive = false;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; RaisePropertyChanged("IsActive"); }
        }

        private Visibility _GridVisibility = Visibility.Collapsed;
        public Visibility GridVisibility
        {
            get { return _GridVisibility; }
            set { _GridVisibility = value; RaisePropertyChanged("GridVisibility"); }
        }

        internal void HideOverlay()
        {
            this.IsActive = false;
            this.GridVisibility = Visibility.Collapsed;

        }

        internal void ShowOverlay()
        {
            this.IsActive = true;
            this.GridVisibility = Visibility.Visible;
        }

        internal MatchingWhistles GetWhistlerById(string id)
        {
            MatchingWhistles selectedWhistle = new MatchingWhistles();
            foreach (MatchingWhistles whistle in this.MatchingWhistles.matchingWhistles)
            {
                if (whistle._id.Equals(id))
                {
                    selectedWhistle = whistle;
                }
            }
            return selectedWhistle;
        }
    }
}
