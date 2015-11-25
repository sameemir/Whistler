using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Whistler.Helpers;
using Whistler.Model;
using Whistler.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Whistler.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INavigationService 
    {
        private WebServiceHandler webServiceHandler;
        private CategoryModel categories;
        private WhistleUser applicationUser;

        public MainPage()
        {
            this.InitializeComponent();
           
            this.Loaded += MainPage_Loaded;
        }

        public MainViewModel ViewModel
        {
            get { return (MainViewModel)this.DataContext; }
        }

        public void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            webServiceHandler = new WebServiceHandler();
            applicationUser = WhistleUser.GetInstance();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {

            if (coneIcon.Tag.Equals("right"))
            {
                coneIcon.Source = new BitmapImage(new Uri("ms-appx:///Assets/coneShapeIcon - left.png"));
                enterNumberAgain.Visibility = Windows.UI.Xaml.Visibility.Visible;
                coneIcon.Tag = "left";
            }
            else
            {
                coneIcon.Tag = "right";
                coneIcon.Source = new BitmapImage(new Uri("ms-appx:///Assets/coneShapeIcon.png"));
                enterNumberAgain.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            coneIcon.Tag = "right";
            coneIcon.Source = new BitmapImage(new Uri("ms-appx:///Assets/coneShapeIcon.png"));
            enterNumberAgain.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public async void LoadAllCategoriesData()
        {
            if (AppData.CheckNetworkConnection())
            {
                this.ViewModel.ShowOverlay();
                string categoriesResponse = await webServiceHandler.GetResponseThroughGet(AppData.AllCategoriesUrl, false);
                categories = JsonConvert.DeserializeObject<CategoryModel>(categoriesResponse);
                
                this.ViewModel.HideOverlay();
                ((Frame)Window.Current.Content).Navigate(typeof(MainCategories),categories);
            }
            else
            {
                MessageDialog msg = new MessageDialog(AppData.NoInternetMessage);
                await msg.ShowAsync();
            }

        }

        private async void doneButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (AppData.CheckNetworkConnection())
            {
                this.ViewModel.ShowOverlay();

                //this.LoadAllCategoriesData();

                HttpClient httpClient = new HttpClient();
                string request = @"{""verify"": """ + textBoxCode.Text + "\"}";

                var content = new StringContent(request, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage responseData = await httpClient.PostAsync(AppData.BaseAddress + "api/user/verifySignup", content);
                if (responseData.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    
                    string whistleUser = await responseData.Content.ReadAsStringAsync();
                    this.applicationUser =  JsonConvert.DeserializeObject<WhistleUser>(whistleUser);

                    if (ApplicationData.Current.LocalSettings.Values.ContainsKey("whistleUser"))
                        ApplicationData.Current.LocalSettings.Values["whistleUser"] = whistleUser;
                       
                    else
                        ApplicationData.Current.LocalSettings.Values.Add("whistleUser", whistleUser);
 

                    this.ViewModel.HideOverlay();
                    this.LoadAllCategoriesData();
                }

                else
                {
                    string responseDataString = await responseData.Content.ReadAsStringAsync();
                    MessageDialog msgBox = new MessageDialog(responseDataString);
                    this.ViewModel.HideOverlay();
                    await msgBox.ShowAsync();
                }
                
                
            }
        }

        public string CurrentPageKey
        {
            get { throw new NotImplementedException(); }
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey)
        {


        }

        private void appBarButtonSignup_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUpPage));
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private void resetButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            textBoxCode.Text = "";
            textboxEnterAgain.Text = "";
        }

        private async void imageButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (AppData.CheckNetworkConnection())
            {
                this.ViewModel.ShowOverlay();
                var content = new StringContent(AppData.SignupRequest, System.Text.Encoding.UTF8, "application/json");
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage responseData = await httpClient.PostAsync(AppData.BaseAddress + "api/user", content);
                if (responseData.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    this.ViewModel.HideOverlay();
                    Frame.Navigate(typeof(MainPage));
                }

                else
                {
                    this.ViewModel.HideOverlay();
                    string responseDataString = await responseData.Content.ReadAsStringAsync();
                    MessageDialog msgBox = new MessageDialog(responseDataString);
                    await msgBox.ShowAsync();
                }
            }
        }
    }
}
