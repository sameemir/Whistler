using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Whistler.Model;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    /// 

    public sealed partial class CreateWhistlePage : Page
    {
        CoreApplicationView view;
        String ImagePath;
        string populate = "";
        public UserModel userModel;

        public CreateWhistlePage()
        {
            this.InitializeComponent();
            view = CoreApplication.GetCurrentView();
            AppData.GetGeolocation();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            userModel = e.Parameter as UserModel;
        }

        private void imageAdd1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.populate = imageAdd1.Name;
            openFilePicker();
        }

        private void openFilePicker()
        {
            ImagePath = string.Empty;
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.ViewMode = PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types
            filePicker.FileTypeFilter.Clear();
            filePicker.FileTypeFilter.Add(".bmp");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".jpg");

            filePicker.PickSingleFileAndContinue();
            view.Activated += view_Activated; 
        }

        async void view_Activated(CoreApplicationView sender, Windows.ApplicationModel.Activation.IActivatedEventArgs args1)
        {
            FileOpenPickerContinuationEventArgs args = args1 as FileOpenPickerContinuationEventArgs;

            if (args != null)
            {
                if (args.Files.Count == 0) return;

                view.Activated -= view_Activated;
                StorageFile storageFile = args.Files[0];
                var stream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                await bitmapImage.SetSourceAsync(stream);

                var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
                
                if(populate.Equals(imageAdd1.Name))
                {
                    imageAdd1.Source = bitmapImage;
                }
                else if (populate.Equals(imageAdd2.Name))
                {
                    imageAdd2.Source = bitmapImage;
                }
                else if (populate.Equals(imageAdd3.Name))
                {
                    imageAdd3.Source = bitmapImage;
                }
                else
                {
                    imageMainWhistle.Source = bitmapImage;
                    imageMainWhistle.Height = (view.CoreWindow.Bounds.Height/2);
                    imageMainWhistle.Width = view.CoreWindow.Bounds.Width;
                }
                
            }
        }

        private void imageAdd2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.populate = imageAdd2.Name;
            openFilePicker();
        }

        private void imageAdd3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.populate = imageAdd3.Name;
            openFilePicker();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            SaveUser();
        }

        private void imageChangePic_Tapped(object sender, TappedRoutedEventArgs e)
        {
            openFilePicker();
            this.populate = imageChangePic.Name;
        }

        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SaveUser();    
        }

        private async void SaveUser()
        {
            
            if (AppData.CheckNetworkConnection())
            {
                if (!ApplicationData.Current.LocalSettings.Values.ContainsKey("whistleUser"))
                {
                    HttpClient httpClient = new HttpClient();
                    string image1, image2, image3;
                    image1 = image2 = image3 = "";

                    if (!imageAdd1.Source.ToString().Contains("AddImage"))
                    {
                        image1 = "http://path.to/image3.ext";
                    }

                    if (!imageAdd2.Source.ToString().Contains("AddImage"))
                    {
                        image2 = "http://path.to/image2.ext";
                    }

                    if (!imageAdd3.Source.ToString().Contains("AddImage"))
                    {
                        image3 = "http://path.to/image3.ext";
                    }

                    string requestString = "{\"user\": {\"name\": \"" + userModel.Name + "\",\"phone\": \"" + userModel.ContactNumber + "\",\"location\": [ 123.45678, 12.3456 ],\"reachability\": {\"call\": " + userModel.CanCallMe.ToString().ToLower() + ",\"SMS\": " + userModel.CanMessageMe.ToString().ToLower() + ",\"email\": " + userModel.CanEmailMe.ToString().ToLower() + "},\"photo\": \"http://path.to/profilepic.ext\",\"category\": \"" + AppData.selectedCategory.name + "\",\"provider\": " + AppData.IsProvider.ToString().ToLower() + ",\"comment\":\"" + textboxComments.Text + "\",\"whistleImages\": [\"" + image1 + "\",\"" + image2 + "\",\"" + image3 + "\"]}}";

                    AppData.SignupRequest = requestString;

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

                else
                {
                    //Add whistle only
                    Frame.Navigate(typeof(MainCategories));
                }
            }

        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            SaveUser();
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            imageChangePic.Visibility = Windows.UI.Xaml.Visibility.Visible;
            openFilePicker();
            this.populate = imageChangePic.Name;
            (sender as Image).Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }


    }
}
