using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Whistler.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
     //       AppData.GetGeolocation();
        }

        private async void SubmitButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            string requestString = "{\"user\": {\"name\": \"" + textboxName.Text + "\",\"phone\": \"" + textboxPhone.Text + "\",\"location\": [31.601712799999998,74.3395395],\"reachability\": {\"call\": " + checkboxCall.IsChecked.ToString().ToLower() +",\"SMS\": " + checkboxSms.IsChecked.ToString().ToLower() +",\"email\": " + checkboxEmail.IsChecked.ToString().ToLower() + "},\"photo\": \"https://en.wikipedia.org/wiki/Taxis_in_India#/media/File:Bangalore_Taxi.jpg\",\"category\": \"taxi\",\"provider\": true,\"comment\": \"Taxeeee!!!\",\"whistleImages\": [\"https://en.wikipedia.org/wiki/Taxis_in_India#/media/File:Bangalore_Taxi.jpg\",\"https://en.wikipedia.org/wiki/Taxis_in_India#/media/File:Bangalore_Taxi.jpg\",\"https://en.wikipedia.org/wiki/Taxis_in_India#/media/File:Taxi_in_Mumbai.jpg\"]}}";

            var content = new StringContent(requestString, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage responseData = await httpClient.PostAsync(AppData.BaseAddress + "api/user", content);
            if (responseData.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Frame.Navigate(typeof(MainPage));
            }

            else
            {
                string responseDataString = await responseData.Content.ReadAsStringAsync();
                MessageDialog msgBox = new MessageDialog(responseDataString);
                await msgBox.ShowAsync();
            }
        }
    }
}
