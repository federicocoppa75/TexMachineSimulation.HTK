using CncViewer.ConfigEditor.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Interfaces
{
    public interface IBinaryInputViewModel : IVariableViewModel<BinaryLinkTarget>
    {
        BinaryInputType BinaryInputType { get; set; }
    }
}
