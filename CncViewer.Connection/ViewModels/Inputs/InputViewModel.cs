using CncViewer.Connection.Interfaces.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.ViewModels.Inputs
{
    public abstract class InputViewModel<T> : VariableViewModel<T>, IInput<T>, IInput
    {
    }
}
