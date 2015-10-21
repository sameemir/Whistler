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

namespace Whistler.ViewModel
{
    public class MainCategoriesViewModel: ViewModelBase
    {
        private WebServiceHandler webServiceHandler;
        private CategoryModel categories;
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
        

    }
}
