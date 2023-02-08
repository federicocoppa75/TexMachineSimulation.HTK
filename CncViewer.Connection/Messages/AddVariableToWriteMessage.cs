using CncViewer.Connection.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.Messages
{
    public class AddVariableToWriteMessage
    {
        public IVariable Variable { get; set; }
    }
}
