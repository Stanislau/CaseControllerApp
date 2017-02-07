using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Danfoss.CaseControllerApp.Apple.ValueConvertors
{
    public class InverseValueConverter : MvxValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return !value;
        }
    }
}