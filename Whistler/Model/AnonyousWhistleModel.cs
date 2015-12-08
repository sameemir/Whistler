using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistler.Model
{
    public class AnonyousWhistleModel
    {
        public AnonyousWhistleModel()
        {
            matchingWhistles = new List<MatchingWhistles>();
        }
        public List<MatchingWhistles> matchingWhistles { get; set; }
        
    }

    public class MatchingWhistles
    {
        public string _id { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
        public string createdAtISO { get; set; }
        public Whistl Whistles { get; set; }
        public List<string> dislikes { get; set; }
        public List<string> likes { get; set; }
        public Reachabilit reachability { get; set; }
        public string usertype { get; set; }
        public string dis { get; set; }
        public Location location { get; set; }
    }

    public class Whistl
    {
        public string _id { get; set; }
        public string category { get; set; }
        public bool provider { get; set; }
        public string comment { get; set; }
        public string createdAtISO { get; set; }
        public List<string> images { get; set; }
        public bool @public { get; set; }
        public bool active { get; set; }
    }

    public class Reachabilit
    {
        public bool email { get; set; }
        public bool SMS { get; set; }
        public bool call { get; set; }
    }
}