using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Interfaces
{
    public interface IInputViewModel : IVariableViewModel
    {
    }

    public interface IInputViewModel<T> : IVariableViewModel<T>, IInputViewModel
    {
    }
}
