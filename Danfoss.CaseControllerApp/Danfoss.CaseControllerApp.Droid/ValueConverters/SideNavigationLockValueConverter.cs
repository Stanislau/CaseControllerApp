using System;
using System.Globalization;
using Android.Support.V4.Widget;
using MvvmCross.Platform.Converters;

namespace Danfoss.CaseControllerApp.Droid.ValueConverters
{
    public class SideNavigationLockValueConverter : MvxValueConverter<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? DrawerLayout.LockModeUnlocked : DrawerLayout.LockModeLockedClosed;
        }
    }
}