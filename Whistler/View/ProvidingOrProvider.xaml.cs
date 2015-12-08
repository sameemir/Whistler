using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Whistler.Model;
using Whistler.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class ProvidingOrProvider : Page
    {
        public MainCategoriesViewModel ViewModel
        {
            get { return (MainCategoriesViewModel)this.DataContext; }
        }
        
        public ProvidingOrProvider()
        {
            this.InitializeComponent();
            this.Loaded += ProvidingOrProvider_Loaded;
        }

        void ProvidingOrProvider_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("isProviderChecked"))
            {
                if((bool)ApplicationData.Current.LocalSettings.Values["isProviderChecked"] == false)
                {
                    AppData.IsProvider = false;
                    Frame.Navigate(typeof(LookingFor));
                }
                else
                {
                    AppData.IsProvider = true;
                    Frame.Navigate(typeof(LookingFor));
                }
            }
            if (this.ViewModel.SelectedCategory.name.Equals("blood"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/BloodDonor_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("autoservice"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/AutoService_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("taxi"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Taxi_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("delivery"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Delivery_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("goldloan"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Loan_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("organicfood"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Organic_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("rent"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Rent_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("ride"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/RideShare_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("service"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Service_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("tickets"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Tickets_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("firstaid"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/FirstAid_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("travel"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/TravelHelp_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("personal"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Personals_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("community"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Community_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("food"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Food_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (this.ViewModel.SelectedCategory.name.Equals("others"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/BloodDonor_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
        }

        private void BorderLookingFor_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AppData.IsProvider = false;
            if(savePreferencesCheckbox.IsChecked.Value)
            {
                if(ApplicationData.Current.LocalSettings.Values.ContainsKey("isProviderChecked"))
                {
                    ApplicationData.Current.LocalSettings.Values["isProviderChecked"] = false;
                }
                else
                {
                    ApplicationData.Current.LocalSettings.Values.Add("isProviderChecked", false);
                }
            }
            Frame.Navigate(typeof(LookingFor));
        }

        private void borderProviding_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AppData.IsProvider = true;
            if (savePreferencesCheckbox.IsChecked.Value)
            {
                if (ApplicationData.Current.LocalSettings.Values.ContainsKey("isProviderChecked"))
                {
                    ApplicationData.Current.LocalSettings.Values["isProviderChecked"] = true;
                }
                else
                {
                    ApplicationData.Current.LocalSettings.Values.Add("isProviderChecked", true);
                }
            }
            Frame.Navigate(typeof(LookingFor));
        }
    }
}
