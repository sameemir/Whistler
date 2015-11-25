using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistler.Model
{
    public class UserModel
    {
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public bool CanCallMe { get; set; }
        public bool CanMessageMe { get; set; }
        public bool CanEmailMe { get; set; }
    }
}
