using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Whistler.Converter
{
    public class DoubleToTwoDigitsConverter: IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double convertedValue = Math.Round(Double.Parse(value.ToString()) , 2);
            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
