using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whistler.Model;

namespace Whistler.ViewModel
{
    public class LookingForViewModel : ViewModelBase
    {
        private string title;
        public LookingForViewModel()
        {
            this.Title = AppData.selectedCategory.label;
        }
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                this.RaisePropertyChanged();
            }
        }

    }
}
