using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace Pizza_App.Converters
{
    public class PizzaNameToImageConverter : IValueConverter
    {
        // Map each pizza name to its corresponding .jpg file in Android's Resources/drawable folder.
        private static readonly Dictionary<string, string> PizzaMap = new Dictionary<string, string>
        {
            { "Pepperoni",      "pizza_pepperoni.jpg" },
            { "Margherita",     "pizza_margherita.jpg" },
            { "Supreme",        "pizza_supreme.jpg" },
            { "Hawaiian",       "pizza_hawaiian.jpg" },
            { "BBQ Chicken",    "pizza_bbqchicken.jpg" }
            // Add more entries as needed.
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 'value' should be the pizza name (e.g., "Pepperoni").
            if (value is string pizzaName && PizzaMap.ContainsKey(pizzaName))
            {
                // Return the matching image file name.
                return PizzaMap[pizzaName];
            }

            // If the pizza name isn't in the dictionary, fallback to a placeholder.
            return "pizza_placeholder.jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not needed for this scenario.
            throw new NotImplementedException();
        }
    }
}
