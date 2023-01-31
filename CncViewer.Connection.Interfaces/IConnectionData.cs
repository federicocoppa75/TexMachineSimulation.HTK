using CncViewer.Connection.Interfaces.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.Interfaces
{
    public interface IConnectionData
    {
       IList<IVariable> Variables { get; }
    }
}
