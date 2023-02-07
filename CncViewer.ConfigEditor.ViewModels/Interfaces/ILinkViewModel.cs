using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Interfaces
{
    public interface ILinkViewModel : IVariableViewModel
    {
        int LinkId { get; }
    }

    public interface ILinkViewModel<T> : IVariableViewModel<T>, ILinkViewModel
    {
    }
}
