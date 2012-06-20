using System;
using System.Globalization;
using System.Windows.Data;

namespace Poker.Calculator.Wpf.Views {
    public class PercentageConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var val = Math.Round((double) value, 3);
            return val.ToString(CultureInfo.CurrentCulture) + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
