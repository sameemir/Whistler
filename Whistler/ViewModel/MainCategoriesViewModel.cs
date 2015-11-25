using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whistler.Helpers;
using Whistler.Model;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace Whistler.ViewModel
{
    public class MainCategoriesViewModel: ViewModelBase
    {
        private WebServiceHandler webServiceHandler;
        private CategoryModel categories;

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
        public Category SelectedCategory { get; set; }
       

        public  MainCategoriesViewModel()
        {
            webServiceHandler = new WebServiceHandler();
        }

        public CategoryModel Categories
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
                this.RaisePropertyChanged();
            }
        }



        internal void ShowOverlay()
        {
            this.IsActive = true;
            this.GridVisibility = Visibility.Visible;
        }

        internal void HideOverlay()
        {
            this.IsActive = false;
            this.GridVisibility = Visibility.Collapsed;
        }
    }
}
