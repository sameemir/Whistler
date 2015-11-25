using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whistler.Model
{
    public class Credentials
    {
        private static readonly Credentials credentials = new Credentials();
        public string cloud_name { get; set; }
        public int timetamp { get; set; }
        public string public_id { get; set; }
        public string api_key { get; set; }
        public string signature { get; set; }

        internal static Credentials getInstance()
        {
            return credentials;
        }
    }

    public class CloudinaryCredentials
    {
        private static readonly CloudinaryCredentials cloudinaryCredentials = new CloudinaryCredentials();

        public static CloudinaryCredentials GetInstance()
        {
           
            return cloudinaryCredentials;
        }
        private CloudinaryCredentials()
        {
            credentials = Credentials.getInstance();
        }
        public Credentials credentials;
    }


}

