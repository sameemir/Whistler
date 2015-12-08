using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Whistler.Model;
using Whistler.ViewModel;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Whistler.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LookingFor : Page
    {
        private AnonyousWhistleModel anonyousWhistleModel;
        
        public LookingFor()
        {
            this.InitializeComponent();
            this.Loaded += LookingFor_Loaded;
        }

        public LookingForViewModel ViewModel
        {
            get { return (LookingForViewModel)this.DataContext; }
        }


        async void LookingFor_Loaded(object sender, RoutedEventArgs e)
        {
            Geolocator geolocator = new Geolocator();
            //await Task.Delay(1500);
            Geoposition geoposition = null;
            try
            {
                geoposition = AppData.GeoPosition;// await geolocator.GetGeopositionAsync();
            }

            catch (Exception ex)
            {
                // Handle errors like unauthorized access to location
                // services or no Internet access.
            }

            while (this.ViewModel.MatchingWhistles == null)
            {
                await Task.Delay(1000);
            }
            foreach (MatchingWhistles whistle in this.ViewModel.MatchingWhistles.matchingWhistles)
            {
                BasicGeoposition whistleLocation = new BasicGeoposition();
                whistleLocation.Longitude = whistle.location.coordinates[0];
                whistleLocation.Latitude = whistle.location.coordinates[1];
                AddPushpin(whistleLocation, whistle.name, whistle._id);
            }

            myMapControl.Center = geoposition.Coordinate.Point;
            myMapControl.ZoomLevel = 15;
          
        }

        public void AddPushpin(BasicGeoposition location, string text, string id)
        {
            var pin = new Grid()
            {
                Width = 50,
                Height = 50,
                Margin = new Windows.UI.Xaml.Thickness(-12)
            };

            pin.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri("ms-appx:///Assets/pinPoint.png", UriKind.RelativeOrAbsolute)),
                Width = 50,
                Height = 50
             });

            pin.Children.Add(new TextBlock()
            {
                Text = text,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.Yellow),
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center
            });

            pin.Tag = id;
            pin.Tapped += pin_Tapped;

            MapControl.SetLocation(pin, new Geopoint(location));
            myMapControl.Children.Add(pin);
        }

        void pin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MatchingWhistles tappedWhistle = this.ViewModel.GetWhistleById((sender as Grid).Tag.ToString());
            //imageDp.Source = tappedWhistle.photo;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (AppData.CheckNetworkConnection())
            {

                overlaGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                GetWhistlers();

               // await Task.Delay(2000);
                overlaGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                MessageDialog msg = new MessageDialog(AppData.NoInternetMessage);
                await msg.ShowAsync();
            }

        }

        private async void GetWhistlers()
        {
            HttpClient httpClient = new HttpClient();

            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("whistleUser"))
            {
                httpClient.BaseAddress = AppData.BaseAddress;
                while(AppData.GeoPosition.Coordinate == null)
                {
                    await Task.Delay(1000);
                }
                string requestString = "{  \"limit\": 10,  \"radius\": 100000,  \"location\": [" + AppData.GeoPosition.Coordinate.Longitude + "," + AppData.GeoPosition.Coordinate.Latitude + "],  \"category\": \"" + AppData.selectedCategory.name + "\",  \"keyword\": \"\"}";
                var content = new StringContent(requestString, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseData = await httpClient.PostAsync("api/whistlerlist", content);


                if (responseData.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string responseDataString = await responseData.Content.ReadAsStringAsync();
                    anonyousWhistleModel = JsonConvert.DeserializeObject<AnonyousWhistleModel>(responseDataString);
                    this.ViewModel.MatchingWhistles = anonyousWhistleModel;
                }

                else
                {
                    string responseDataString = await responseData.Content.ReadAsStringAsync();
                    MessageDialog msgBox = new MessageDialog(responseDataString);
                    await msgBox.ShowAsync();
                }
            }
            else
            {
                // get list through auth
            }
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

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WhistlerList));
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private async void imageMessage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Windows.ApplicationModel.Chat.ChatMessage msg = new Windows.ApplicationModel.Chat.ChatMessage();
            msg.Recipients.Add((sender as Image).Tag.ToString());
            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(msg);
        }

        private void imagecall_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI((sender as Image).Tag.ToString(), "");
        }

    }
}
