using GalaSoft.MvvmLight.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Whistler.Helpers;
using Whistler.Model;
using Whistler.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        private void doneButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            LoadAllCategoriesData();
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
    }
}
