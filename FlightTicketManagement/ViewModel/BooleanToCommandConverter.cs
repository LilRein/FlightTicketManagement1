using System;
using System.Globalization;
using System.Windows.Data;

namespace FlightTicketManagement.ViewModel
{
    public class BooleanToCommandConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolean)
            {
                return boolean ? "EditCommand" : "ConfirmCommand";
            }
            return "ConfirmCommand";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}