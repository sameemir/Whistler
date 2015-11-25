using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Whistler.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LookingFor : Page
    {
        public LookingFor()
        {
            this.InitializeComponent();
            this.Loaded += LookingFor_Loaded;
        }

        async void LookingFor_Loaded(object sender, RoutedEventArgs e)
        {
            Geolocator geolocator = new Geolocator();
            Geoposition geoposition = null;
            try
            {
                geoposition = await geolocator.GetGeopositionAsync();
            }
            catch (Exception ex)
            {
                // Handle errors like unauthorized access to location
                // services or no Internet access.
            }
            myMapControl.Center = geoposition.Coordinate.Point;
            myMapControl.ZoomLevel = 15;
          
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            overlaGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            overlaGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
           
        }

        private void textboxCreateWhistler_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
                Frame.Navigate(typeof(MyProfile));
        }

        private void textboxChangeCategory_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainCategories));
        }
    }
}
