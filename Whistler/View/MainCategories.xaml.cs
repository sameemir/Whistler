using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Whistler.Helpers;
using Whistler.Model;
using Whistler.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
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
    public sealed partial class MainCategories : Page
    {
        private WebServiceHandler webServiceHandler;
        private CategoryModel categories;
        private WhistleUser appUser;
        public MainCategoriesViewModel ViewModel
        {
            get { return (MainCategoriesViewModel)this.DataContext; }
        }
        public MainCategories()
        {
            this.InitializeComponent();
            this.Loaded += MainCategories_Loaded;

            AppData.GetGeolocation();
            
            webServiceHandler = new WebServiceHandler();

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("whistleUser"))
                appUser = JsonConvert.DeserializeObject<WhistleUser>(ApplicationData.Current.LocalSettings.Values["whistleUser"].ToString());
            
        }

        async void MainCategories_Loaded(object sender, RoutedEventArgs e)
        {
            if (AppData.CheckNetworkConnection())
            {
                this.ViewModel.ShowOverlay();
                string categoriesResponse = await webServiceHandler.GetResponseThroughGet(AppData.AllCategoriesUrl, false);
                categories = JsonConvert.DeserializeObject<CategoryModel>(categoriesResponse);

                this.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                this.ViewModel.HideOverlay();
                this.ViewModel.Categories = categories;
            }
            else
            {
                MessageDialog msg = new MessageDialog(AppData.NoInternetMessage);
                await msg.ShowAsync();
            }
           
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Category category = ((FrameworkElement)sender).DataContext as Category;
            this.ViewModel.SelectedCategory = category;
            AppData.selectedCategory = category;
            this.NavigateToDetailPage(category);
        }

        private void NavigateToDetailPage(Category category)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(CategoryDetails));
        }

        private void appBarButtonMore_Tapped(object sender, TappedRoutedEventArgs e)
        {
           
        }

        private void appBarButtonMore_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MorePage));
        }
    }
}
