using CncViewer.Connection.Interfaces;
using CncViewer.Connection.Interfaces.Enums;
using CncViewer.Connection.Interfaces.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncViewer.Connection.Views.Selectors
{
    internal class VariableValueTemplateSelector : TemplateSelector<VariableType>
    {
        protected override bool TryGetPropertyToCompare(object item, out VariableType value)
        {
            bool result = false;

            if (item is IVariable v)
            {
                value = v.VariableType;
                result = true;
            }
            else
            {
                value = default;
            }

            return result;
        }
    }

    class VariableValueTemplateSelectorItem : TemplateSelectorItem<VariableType>
    {
    }
}
