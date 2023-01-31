using System;
using System.Collections.Generic;
using System.Text;
using MVMIL = Machine.ViewModels.Interfaces.Links;

namespace CncViewer.Connection.ViewModels.Links
{
    public class BinaryVariableViewModel : VariableViewModel<bool, MVMIL.IPneumaticLinkViewModel>
    {
        protected override void OnValueChanged()
        {
            if (_link != null) _link.State = Value;
        }
    }
}
