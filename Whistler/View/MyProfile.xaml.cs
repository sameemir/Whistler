using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Whistler.Model;
using Whistler.ViewModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
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
    public sealed partial class MyProfile : Page
    {
        CoreApplicationView view;
        String ImagePath;
        public MyProfile()
        {
            this.InitializeComponent();
            view = CoreApplication.GetCurrentView();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
              if (ApplicationData.Current.LocalSettings.Values.ContainsKey("whistleUser"))
              {
                  (this.DataContext as MyProfileViewModel).AppUser = JsonConvert.DeserializeObject<WhistleUser>(ApplicationData.Current.LocalSettings.Values["whistleUser"].ToString());
              }
        }

        private void buttonAddImage_Tapped(object sender, TappedRoutedEventArgs e)
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
            view.Activated += viewActivated; 
        }

        private async void viewActivated(CoreApplicationView sender, Windows.ApplicationModel.Activation.IActivatedEventArgs args1)
        {
            FileOpenPickerContinuationEventArgs args = args1 as FileOpenPickerContinuationEventArgs;

            if (args != null)
            {
                if (args.Files.Count == 0) return;

                view.Activated -= viewActivated;
                StorageFile storageFile = args.Files[0];

                fileBytes = null;
                var stream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);

                

                var bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                
                await bitmapImage.SetSourceAsync(stream);

                fileBytes = new byte[stream.Size];

                await stream.ReadAsync(fileBytes.AsBuffer(), (uint)stream.Size, Windows.Storage.Streams.InputStreamOptions.None);

                var decoder = await Windows.Graphics.Imaging.BitmapDecoder.CreateAsync(stream);
                imageDp.Source = bitmapImage;

                
               
                imageDp.Height = CoreApplication.GetCurrentView().CoreWindow.Bounds.Height;
                imageDp.Width = CoreApplication.GetCurrentView().CoreWindow.Bounds.Width;
                buttonAddImage.Content = "change image";
            }
        }

        private void resetButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            buttonAddImage.Content = "add image";
            imageDp.Source = null;
            textboxName.Text = "";
            textboxPhone.Text = "";
        }

        private async void doneButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            UserModel userModel = new UserModel();

            if (imageDp.Source == null || textboxName.Text == "" || textboxPhone.Text == "")
            {
                MessageDialog message = new MessageDialog("Please fill in all fields");
                await message.ShowAsync();
            }
            else
            {
                userModel.ImagePath = imageDp.Source.ToString();
                userModel.Name = textboxName.Text;
                userModel.ContactNumber = textboxPhone.Text;
                userModel.CanCallMe = (bool)radioButtonShowPhone.IsChecked;
                userModel.CanMessageMe = (bool)radioButtonShowSMS.IsChecked;
                userModel.CanEmailMe = false;

                string credentials = await GetCredentialsForCloudinary();

                CloudinaryCredentials CloudinaryCredentials = CloudinaryCredentials.GetInstance();

                CloudinaryCredentials = JsonConvert.DeserializeObject<CloudinaryCredentials>(credentials);

                UploadDPToCloundinary();

                Frame.Navigate(typeof(CreateWhistlePage), userModel);
            }
        }

        private async void UploadDPToCloundinary()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = AppData.CloudinaryUrl;
            ByteArrayContent content = new ByteArrayContent(fileBytes);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
            MultipartFormDataContent form = new MultipartFormDataContent();

            ////form.Add(new StringContent("http://www.controltechinc.com/news/wp-content/uploads/path-choise.jpg"), "file", Guid.NewGuid().ToString() + ".jpg");

            CloudinaryRequest request = new CloudinaryRequest();
            request.file = Convert.ToBase64String(fileBytes);
            request.signature = CloudinaryCredentials.GetInstance().credentials.signature;
            request.api_key = CloudinaryCredentials.GetInstance().credentials.api_key;
            request.public_id = CloudinaryCredentials.GetInstance().credentials.public_id;
            request.timestamp = CloudinaryCredentials.GetInstance().credentials.timetamp.ToString();

            string postBody = JsonConvert.SerializeObject(request); 

            //form.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Count()));
            //form.Add(new StringContent(CloudinaryCredentials.GetInstance().credentials.api_key), "api_key");
            //form.Add(new StringContent(CloudinaryCredentials.GetInstance().credentials.timetamp.ToString()), "timestamp");
            //form.Add(new StringContent(CloudinaryCredentials.GetInstance().credentials.signature), "signature");
            //form.Add(new StringContent(CloudinaryCredentials.GetInstance().credentials.public_id), "public_id");

            //httpClient.DefaultRequestHeaders.Add("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"file\"\r\n\r\nhttp://facewallpaper.com/wp-content/uploads/2015/10/Cool-Wallpaper-COWA045.jpg\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"api_key\"\r\n\r\n877962912131272\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"timestamp\"\r\n\r\n1448545328\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"signature\"\r\n\r\n7bf9de1286426d42a332a5abff43a94a9a4bbfc9\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"public_key\"\r\n\r\nhhhh\r\n-----011000010111000001101001--");


            HttpResponseMessage response = await httpClient.PostAsync(CloudinaryCredentials.GetInstance().credentials.cloud_name + "/image/upload",new StringContent(postBody, Encoding.UTF8, "application/json"));
            string serverResponse = await response.Content.ReadAsStringAsync();

            if (serverResponse != null)
            {

            }


            //var client = new RestClient("https://api.cloudinary.com/v1_1/hbfzopaol/image/upload");
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("postman-token", "d7916932-1aca-dbbd-2094-b40cae1e366c");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
            //request.AddParameter("multipart/form-data; boundary=---011000010111000001101001", "-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"file\"\r\n\r\nhttp://facewallpaper.com/wp-content/uploads/2015/10/Cool-Wallpaper-COWA045.jpg\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"api_key\"\r\n\r\n877962912131272\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"timestamp\"\r\n\r\n1448545328\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"signature\"\r\n\r\n7bf9de1286426d42a332a5abff43a94a9a4bbfc9\r\n-----011000010111000001101001\r\nContent-Disposition: form-data; name=\"public_key\"\r\n\r\nhhhh\r\n-----011000010111000001101001--", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);

        }

        private async Task<string> GetCredentialsForCloudinary()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = AppData.BaseAddress;
            HttpResponseMessage message;
            if (!String.IsNullOrEmpty(CloudinaryCredentials.GetInstance().credentials.public_id))
                message = await httpClient.GetAsync("api/cloudinaryAuth/" + CloudinaryCredentials.GetInstance().credentials.public_id);
            else
                message = await httpClient.GetAsync("api/cloudinaryAuth/" + "564cdb6876e98b97013f5704");

            string response = await message.Content.ReadAsStringAsync();

            return response;
        }

        private void buttonAddImage_Loaded(object sender, RoutedEventArgs e)
        {
            if((sender as Button).Tag!=null)
            {
                (sender as Button).Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void textboxPhone_Loaded(object sender, RoutedEventArgs e)
        {
            if((sender as TextBox).Text != "")
            {
                (sender as TextBox).IsReadOnly = true;
            }
        }

        public IBuffer ImageBuffer { get; set; }

        public byte[] fileBytes { get; set; }
    }
}
