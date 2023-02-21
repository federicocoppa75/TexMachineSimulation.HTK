using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace CncViewer.Connection.Views.Converters
{
    [ContentProperty("Values")]
    internal class TypeConverter<T, Y> : IValueConverter where T : struct
    {
        public List<TypeConverterItem<T, Y>> Values { get; set; } = new List<TypeConverterItem<T, Y>>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;

            if (value is T v)
            {
                foreach (var item in Values)
                {
                    if (item.When.Equals(v))
                    {
                        result = item.Then;
                        break;
                    }
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ContentProperty("Then")]
    class TypeConverterItem<T, Y> where T : struct
    {
        public T When { get; set; }
        public Y Then { get; set; }
    }
}
