using CncViewer.ConfigEditor.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Links
{
    public class BinaryLinkViewModel : LinkViewModel
    {
        private BinaryLinkTarget _binaryLinkTarget;
        public BinaryLinkTarget BinaryLinkTarget
        { 
            get => _binaryLinkTarget;
            set
            {
                if(Set(ref _binaryLinkTarget, value, nameof(BinaryLinkTarget)))
                {
                    RisePropertyChanged(nameof(TargetType));
                }
            }
        }

        public override TargetType TargetType => _binaryLinkTarget == BinaryLinkTarget.Out ? TargetType.Out : TargetType.Flag;

        public BinaryLinkViewModel(int linkId) : base(linkId)
        {
        }
    }
}
