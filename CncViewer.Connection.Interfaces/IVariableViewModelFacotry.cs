using CncViewer.Connection.Interfaces.Enums;
using CncViewer.Connection.Interfaces.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.Interfaces
{
    public interface IVariableViewModelFacotry
    {
        IVariable Create(VariableType type, int index, int linkId, string description, double factor = 0);
    }
}
