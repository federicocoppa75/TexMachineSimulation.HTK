using CncViewer.ConfigEditor.ViewModels.Enums;
using CncViewer.ConfigEditor.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Links
{
    public class LinearLinkViewModel : LinkViewModel<LinearLinkTarget>, ILinearLinkViewModel
    {
        public override TargetType TargetType => (SelectableType == LinearLinkTarget.DWord) ? TargetType.DWord : TargetType.Word;

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
