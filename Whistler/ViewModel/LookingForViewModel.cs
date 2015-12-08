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
        private AnonyousWhistleModel matchingWhistles;
        public LookingForViewModel()
        {
            this.Title = AppData.selectedCategory.label;
        }

        public MatchingWhistles GetWhistleById(string id)
        {
            MatchingWhistles matchedWhistle = new MatchingWhistles();
            foreach (MatchingWhistles whistle in this.MatchingWhistles.matchingWhistles)
            {
                if (whistle._id.Equals(id))
                {
                    matchedWhistle = whistle;
                }
            }
            return matchedWhistle;
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


        public AnonyousWhistleModel MatchingWhistles
        {
            get { return this.matchingWhistles; }
            set
            {
                this.matchingWhistles = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
