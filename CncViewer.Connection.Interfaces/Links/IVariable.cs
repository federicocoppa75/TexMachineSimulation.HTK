using CncViewer.Connection.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.Interfaces.Links
{
    public interface IVariable
    {
        int LinkId { get; }
        VariableType VariableType { get; }
        int Index { get; }
        string Description { get; }

        void SetValue<V>(V value);
    }

    public interface IVariable<T> : IVariable
    {
        T Value { get; set; }
    }
}
