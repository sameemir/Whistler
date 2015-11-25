using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Whistler.Model;

namespace Whistler.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
//        StringContent content = new StringContent("{\"user\": {\"name\": \"Gabbar Singh\",\"phone\": \"+911234567890\",\"location\": [ 123.45678, 12.3456 ],\"reachability\": {\"call\": true,\"SMS\": false,\"email\": false         },         \"photo\": \"http://path.to/profilepic.ext\",         /* Whistle Information */         \"category\": \"taxi\",         \"provider\": true,         \"comment\": \"Taxeeee!!!\",         \"whistleImages\": [           \"http://path.to/image1.ext\",           \"http://path.to/image2.ext\",           \"http://path.to/image3.ext\"         ]     } }", System.Text.Encoding.UTF8, "application/json");

        private async void RegisterUser(StringContent content)
        {
            var baseAddress = AppData.BaseAddress;

            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                {
                    using (var response = await httpClient.PostAsync("api/user", content))
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                    }
                }
            }
        }
    }
}
