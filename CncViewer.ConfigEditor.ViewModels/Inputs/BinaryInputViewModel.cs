using CncViewer.ConfigEditor.ViewModels.Enums;
using CncViewer.ConfigEditor.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Inputs
{
    public class BinaryInputViewModel : InputViewModel, IBinaryInputViewModel
    {
        public override TargetType TargetType => TargetType.Flag;

        private BinaryInputType _binaryInputType;
        public BinaryInputType BinaryInputType 
        { 
            get => _binaryInputType; 
            set => Set(ref _binaryInputType, value, nameof(BinaryInputType));
        }
    }
}
