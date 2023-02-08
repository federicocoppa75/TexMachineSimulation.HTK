using CncViewer.Connection.Interfaces.Enums;
using CncViewer.Connection.Interfaces.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncViewer.Connection.Views.Selectors
{
    internal class BinaryRequestValueTemplateSelector : TemplateSelector<BinaryInputType>
    {
        protected override bool TryGetPropertyToCompare(object item, out BinaryInputType value)
        {
            bool result = false;

            if(item is IBinaryInput bi)
            {
                value = bi.BinaryInputType;
                result= true;
            }
            else 
            {
                value = default(BinaryInputType);
            }

            return result;
        }
    }

    internal class BinaryRequestValueTemplateSelectorItem :  TemplateSelectorItem<BinaryInputType>
    {
    }
}
