using System;
using System.Globalization;
using System.Windows.Data;

namespace Poker.Views {
    public class PercentageConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var val = (double)value;
            return val.ToString(CultureInfo.CurrentCulture) + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
