using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whistler.Model;

namespace Whistler.ViewModel
{
    public class MyProfileViewModel: ViewModelBase
    {
        private WhistleUser appUser;

        public WhistleUser AppUser
        {
            get
            {
                return this.appUser;
            }

            set
            {
                this.appUser = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
