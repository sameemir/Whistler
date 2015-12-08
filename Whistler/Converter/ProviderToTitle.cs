using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whistler.Model;
using Windows.UI.Xaml.Data;

namespace Whistler.Converter
{
    public class ProviderToTitle: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(AppData.IsProvider)
            {
                return "Providing ";
            }
            else
            {
                return "Looking For ";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
