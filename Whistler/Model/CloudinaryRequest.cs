using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistler.Model
{
    public class CloudinaryRequest
    {
        public string file { get; set; }
        public string api_key { get; set; }
        public string timestamp { get; set; }
        public string signature { get; set; }
        public string public_id { get; set; }


    }
}
