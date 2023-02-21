using CncViewer.Connection.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncViewer.Connection.Views.Converters
{
    internal class VariableTypeToStringConverter : TypeConverter<VariableType, string>
    {
    }

    class VariableTypeToStringConverterItem : TypeConverterItem<VariableType, string>
    {
    }
}
