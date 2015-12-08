using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Whistler.Helpers;
using Whistler.Model;
using Whistler.ViewModel;
using Windows.UI;
using Windows.UI.Popups;
using Newtonsoft.Json;
using System.Net.Http;
using Windows.Storage;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Whistler.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WhistlerList : Page
    {
        
        private WebServiceHandler webServiceHandler;
        private AnonyousWhistleModel anonyousWhistleModel;
        
        public WhistlerListViewModel ViewModel
        {
            get { return (WhistlerListViewModel)this.DataContext; }
        }

        public WhistlerList()
        {
            this.InitializeComponent();
            this.Loaded += MainCategories_Loaded;

        }

       void MainCategories_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
      
        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
       protected async override void OnNavigatedTo(NavigationEventArgs e)
       {
           if (AppData.CheckNetworkConnection())
           {
               this.ViewModel.ShowOverlay();

               GetWhistlers();

               this.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
               this.ViewModel.HideOverlay();
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
                string requestString = "{  \"limit\": 10,  \"radius\": 100000,  \"location\": ["+AppData.GeoPosition.Coordinate.Longitude+"," + AppData.GeoPosition.Coordinate.Latitude + "],  \"category\": \""+AppData.selectedCategory.name+"\",  \"keyword\": \"\"}";
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        #endregion

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel.MatchingWhistles != null && this.ViewModel.MatchingWhistles.matchingWhistles != null)
            {
                int count = this.ViewModel.MatchingWhistles.matchingWhistles.Count;
                (sender as TextBlock).Text = count + " Whistlers Found";
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void textBlockDistance_Loaded(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty((sender as TextBlock).Text))
            {
                (sender as TextBlock).Text = Math.Round(Convert.ToDecimal((sender as TextBlock).Text),2) + "Kms";
            }
        }

        private async void imageSendMessage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Windows.ApplicationModel.Chat.ChatMessage msg = new Windows.ApplicationModel.Chat.ChatMessage();
            msg.Recipients.Add((sender as Image).Tag.ToString());
            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(msg);
        }

        private void imageCall_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI((sender as Image).Tag.ToString(), "");
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(WhistlerDetail), this.ViewModel.GetWhistlerById((sender as Border).Tag.ToString()));
        }
    }
}