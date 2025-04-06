using System;
using System.Globalization;
using Xamarin.Forms;

namespace Pizza_App.Converters
{
    // Converts a pizza name to its corresponding image file name.
    // For example, "Margherita" becomes "pizza_margherita.jpg".
    public class PizzaImageConverter : IValueConverter
    {
        // Convert the pizza name to an image file name.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string pizzaName)
            {
                // Create the image file name in the format: "pizza_{pizzaname}.jpg"
                // This example converts the pizza name to lowercase and removes spaces.
                string formattedName = pizzaName.ToLower().Replace(" ", "");
                return $"pizza_{formattedName}.jpg";
            }
            // Fallback image if the value is not valid.
            return "pizza_placeholder.jpg";
        }

        // Not implemented as two-way binding is not needed.
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
