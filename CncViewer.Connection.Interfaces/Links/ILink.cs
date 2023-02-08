using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.Interfaces.Links
{
    public interface ILink : IVariable
    {
        int LinkId { get; }
    }

    public interface ILink<T> : ILink, IVariable<T>
    {
    }
}
