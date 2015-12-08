using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.System;

namespace Whistler.Model
{
    public static class AppData
    {
        static AppData()
        {

            CloudinaryUrl = new Uri("https://api.cloudinary.com/v1_1/");

            //AllCategoriesUrl = "http://api.dowhistle.com/api/categories";
            AllCategoriesUrl = "http://api.dowhistle.com/api/categories";
            NoInternetMessage = "No Internet available! Please check your internet connection";
//            BaseAddress = new Uri("http://api.dowhistle.com/");
            BaseAddressLive = new Uri("http://api.dowhistle.com/");
            BaseAddress = new Uri("http://whistle-dev.herokuapp.com/");
            GeoLocation = null;

        }
        /// class for all the constants in applicaton
        /// 
        public static bool CheckNetworkConnection()
        {
            bool IsConnected = false;
            IsConnected = NetworkInterface.GetIsNetworkAvailable();

            return IsConnected;
        }

        public static Uri CloudinaryUrl { get; set; }

        public static async void GetGeolocation()
        {
            bool launchLocationSettings = false;
            GeoLocation = new Geolocator();
            // Desired Accuracy needs to be set     
            // before polling for desired accuracy.     
            GeoLocation.DesiredAccuracyInMeters = 50;
            GeoLocation.MovementThreshold = 5;
            GeoLocation.ReportInterval = 500;
            try
            {
                // Carry out the operation  Geoposition pos =    
                GeoPosition = await GeoLocation.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5),
                timeout: TimeSpan.FromSeconds(10));
            }
            catch
            {
                /*Operation aborted Your App does not have permission to access location     data.  Make sure you have defined ID_CAP_LOCATION in the    application manifest and that on your phone,  you have turned on location by checking Settings > Location.*/
                //if the location service is turned off then you can take the user to the   location setting with the below code      
                launchLocationSettings = true;
            }
            if (launchLocationSettings)
            {
                await Launcher.LaunchUriAsync(new Uri("ms-settings-location:"));
                launchLocationSettings = false;
            }
        }

        public static Geolocator GeoLocation { get; set; }
        public static string AllCategoriesUrl { get; set; }
        public static string AuthorizationKey { get; set; }// will be populated after successfull login.
        public static string NoInternetMessage { get; set; }
        public static Uri BaseAddress { get; set; }
        public static Geoposition GeoPosition { get; set; }

        public static Category selectedCategory { get; set; }

        public static String selectedSubCategory { get; set; }
        
        public static bool IsProvider { get; set; }

        public static string SignupRequest { get; set; }

        public static Uri BaseAddressLive { get; set; }
    }
}
