using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Whistler.Model
{
    public static class AppData
    {
        static AppData()
        {
            AllCategoriesUrl = "http://api.dowhistle.com/api/categories";
            NoInternetMessage = "No Internet available! Please check your internet connection";
        }
        /// class for all the constants in applicaton
        /// 
        public static bool CheckNetworkConnection()
        {
            bool IsConnected = false;
            IsConnected = NetworkInterface.GetIsNetworkAvailable();

            return IsConnected;
        }

        public static string AllCategoriesUrl { get; set; }

        public static string AuthorizationKey { get; set; }// will be populated after successfull login.

        public static string NoInternetMessage { get; set; }
    }
}
