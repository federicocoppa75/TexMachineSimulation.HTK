using CncViewer.Connection.Interfaces.Enums;
using CncViewer.Connection.Interfaces.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.ViewModels.Inputs
{
    public class BinaryInputViewModel : InputViewModel<bool>, IBinaryInput
    {
        public BinaryInputType BinaryInputType { get; set; }

        protected override void OnValueChanged()
        {
        }
    }
}
