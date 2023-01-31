using CncViewer.ConfigEditor.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Links
{
    public class LinearLinkViewModel : LinkViewModel
    {
        private LinearLinkTarget _linearTargetType;
        public LinearLinkTarget LinearTargetType
        {
            get => _linearTargetType;
            set
            {
                if(Set(ref _linearTargetType, value, nameof(LinearTargetType)))
                {
                    RisePropertyChanged(nameof(TargetType));
                }
            }
        }


        public override TargetType TargetType => (_linearTargetType == LinearLinkTarget.DWord) ? TargetType.DWord : TargetType.Word;

        private double _factor = 1.0;
        public double Factor
        {
            get => _factor; 
            set => Set(ref _factor,value, nameof(Factor));
        }


        public LinearLinkViewModel(int linkId) : base(linkId)
        {
        }
    }
}
