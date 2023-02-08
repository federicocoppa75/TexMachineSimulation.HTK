using CncViewer.ConfigEditor.ViewModels.Enums;
using CncViewer.ConfigEditor.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Inputs
{
    public abstract class InputViewModel : VariableViewModel<BinaryLinkTarget>, IInputViewModel
    {
        protected override void OnSelectableTypeChanged() => RisePropertyChanged(nameof(TargetType));
    }
}
