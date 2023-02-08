using CncViewer.Connection.Interfaces.Enums;
using CncViewer.Connection.Interfaces.Inputs;
using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CncViewer.Connection.ViewModels.Inputs
{
    public class BinaryInputViewModel : InputViewModel<bool>, IBinaryInput
    {
        public BinaryInputType BinaryInputType { get; set; }

        private ICommand _setValueCommand;
        public ICommand SetValueCommand => _setValueCommand ?? (_setValueCommand = new RelayCommand(() => RequestValue = true ));

        private ICommand _resetValueCommand;
        public ICommand ResetValueCommand => _resetValueCommand ?? (_resetValueCommand = new RelayCommand(() => RequestValue = false));

        protected override void OnValueChanged()
        {
            RequestValue = Value;
        }
    }
}
