using System;
using System.Globalization;
using Xamarin.Forms;

namespace Pizza_App.Converters
{
    public class SelectionToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string selectedValue = value?.ToString();
            string current = parameter?.ToString();

            return selectedValue == current
                ? Application.Current.Resources["PrimaryColor"]
                : Application.Current.Resources["SurfaceColor"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
