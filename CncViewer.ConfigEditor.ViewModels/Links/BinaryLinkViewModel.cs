using CncViewer.ConfigEditor.ViewModels.Enums;
using CncViewer.ConfigEditor.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Links
{
    public class BinaryLinkViewModel : LinkViewModel<BinaryLinkTarget>, IBinaryLinkViewModel
    {
        public override TargetType TargetType => SelectableType == BinaryLinkTarget.Out ? TargetType.Out : TargetType.Flag;

        public BinaryLinkViewModel(int linkId) : base(linkId)
        {
        }
    }
}
