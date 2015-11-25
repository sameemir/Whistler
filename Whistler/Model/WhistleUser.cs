using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistler.Model
{
    public class Whistle
    {
        public string _id { get; set; }
        public string category { get; set; }
        public bool provider { get; set; }
        public string comment { get; set; }
        public List<string> images { get; set; }
        public long createdAt { get; set; }
        public string lastUpdatedAt { get; set; }
        public bool active { get; set; }
        public bool @public { get; set; }
        public string createdAtISO { get; set; }
    }

    public class Location
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Reachability
    {
        public bool call { get; set; }
        public bool SMS { get; set; }
    }

    public class NewUser
    {
        public string _id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string photo { get; set; }
        public long createdAt { get; set; }
        public string lastLogin { get; set; }
        public List<Whistle> Whistles { get; set; }
        public List<object> dislikes { get; set; }
        public List<object> likes { get; set; }
        public Location location { get; set; }
        public Reachability reachability { get; set; }
        public string usertype { get; set; }
        public string accessToken { get; set; }
        public bool verified { get; set; }
        public bool visible { get; set; }
        public bool teaser { get; set; }
        public string createdAtISO { get; set; }
    }

    public class WhistleUser
    {
        private static WhistleUser whistleUser;
        public static WhistleUser GetInstance()
        {
            if (whistleUser == null)
                whistleUser = new WhistleUser();
            return whistleUser;
        }

        private WhistleUser()
        {

        }
        public NewUser newUser { get; set; }
    }
}
