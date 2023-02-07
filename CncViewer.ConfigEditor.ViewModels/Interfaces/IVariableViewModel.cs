using CncViewer.ConfigEditor.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Interfaces
{
    public interface IVariableViewModel
    {
        TargetType TargetType { get; }
        int Index { get; set; }
        string Description { get; set; }
    }

    public interface IVariableViewModel<T> : IVariableViewModel
    {
        T SelectableType { get; set; }
    }
}
