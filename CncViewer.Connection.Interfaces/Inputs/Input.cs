using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.Interfaces.Inputs
{
    public interface IInput : IVariable
    {
    }

    public interface IInput<T> : IInput, IVariable<T> 
    {
    }
}
