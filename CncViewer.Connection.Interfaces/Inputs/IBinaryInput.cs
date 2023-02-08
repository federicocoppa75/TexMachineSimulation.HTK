using CncViewer.Connection.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.Interfaces.Inputs
{
    public interface IBinaryInput : IInput<bool>
    {
        BinaryInputType BinaryInputType { get; }
    }
}
