using System;
using System.Collections.Generic;
using System.Text;
using MVMIL = Machine.ViewModels.Interfaces.Links;

namespace CncViewer.Connection.ViewModels.Links
{
    public class FixedPointVariableViewModel : VariableViewModel<float, MVMIL.ILinearLinkViewModel>
    {
        public double Factor { get; set; }

        protected override void OnValueChanged()
        {
            if (_link != null) _link.Value = Value * Factor;
        }
    }
}
