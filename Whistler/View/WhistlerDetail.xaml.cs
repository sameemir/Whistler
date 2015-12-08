using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Whistler.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class WhistlerDetail : Page
    {
        MatchingWhistles selectedWhistler;
        public WhistlerDetail()
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
            selectedWhistler= (e.Parameter as MatchingWhistles);
            if (selectedWhistler.photo != null && !String.IsNullOrEmpty(selectedWhistler.photo))
            {
                userImage.ImageSource = new BitmapImage(new Uri(selectedWhistler.photo, UriKind.RelativeOrAbsolute));
            }

            if (AppData.selectedCategory.name.Equals("blood"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/BloodDonor_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("autoservice"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/AutoService_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("taxi"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Taxi_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("delivery"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Delivery_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("goldloan"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Loan_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                this.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("organicfood"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Organic_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("rent"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Rent_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("ride"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/RideShare_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("service"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Service_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("tickets"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Tickets_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("firstaid"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/FirstAid_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("travel"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/TravelHelp_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("personal"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Personals_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("community"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Community_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("food"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/Food_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
            else if (AppData.selectedCategory.name.Equals("others"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/BloodDonor_SC.jpg", UriKind.Absolute));
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = bitmapImage;
                userNameGrid.Background = imageBrush;
            }
        }

        private async void messageTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(selectedWhistler.phone))
            {
                Windows.ApplicationModel.Chat.ChatMessage msg = new Windows.ApplicationModel.Chat.ChatMessage();
                msg.Recipients.Add(selectedWhistler.phone);
                await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(msg);
            }
        }

        private void callTextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(selectedWhistler.phone))
            {
                Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(selectedWhistler.phone, "");
            }
        }

        private void textBlockLookingFor_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void textBlockHeading_Loaded(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(selectedWhistler.Whistles.category))
            {
                (sender as TextBlock).Text = (sender as TextBlock).Text + " " + selectedWhistler.Whistles.category;
            }
        }

        private void nameTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(selectedWhistler.name))
            {
                (sender as TextBlock).Text = selectedWhistler.name;
            }
        }

        private void disLikeTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (selectedWhistler != null)
            {
                int count = selectedWhistler.dislikes.Count;
                (sender as TextBlock).Text = count + "";
            }
        }

        private void likeTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (selectedWhistler != null)
            {
                int count = selectedWhistler.likes.Count;
                (sender as TextBlock).Text = count + "";
            }
        }

        private void txtBlockDistance_Loaded(object sender, RoutedEventArgs e)
        {
            if (selectedWhistler != null && !String.IsNullOrEmpty(selectedWhistler.dis))
            {
                (sender as TextBlock).Text = Math.Round(Convert.ToDouble(selectedWhistler.dis),2) + (sender as TextBlock).Text;
            }
        }

        private void detailTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (selectedWhistler != null && !String.IsNullOrEmpty(selectedWhistler.Whistles.comment))
            {
                (sender as TextBlock).Text = selectedWhistler.Whistles.comment;
            }
        }

        private void imageListItem1_Loaded(object sender, RoutedEventArgs e)
        {
            if (selectedWhistler != null && selectedWhistler.Whistles.images.Count > 0 && !String.IsNullOrEmpty(selectedWhistler.Whistles.images[0]));
            {
                if (selectedWhistler.Whistles.images.Count > 0)
                {
                    (sender as Image).Source = new BitmapImage(new Uri(selectedWhistler.Whistles.images[0], UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void imageListItem2_Loaded(object sender, RoutedEventArgs e)
        {
            if (selectedWhistler != null && selectedWhistler.Whistles.images.Count > 1 && !String.IsNullOrEmpty(selectedWhistler.Whistles.images[1]))
            {
                (sender as Image).Source = new BitmapImage(new Uri(selectedWhistler.Whistles.images[1], UriKind.RelativeOrAbsolute));
            }
        }

        private void textBlockLookingFor_Loaded(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(selectedWhistler.Whistles.category))
            {
                (sender as TextBlock).Text = (sender as TextBlock).Text + "" + AppData.selectedSubCategory;
            }
        }
    }
}