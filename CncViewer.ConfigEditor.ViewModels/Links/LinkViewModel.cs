using CncViewer.ConfigEditor.ViewModels.Enums;
using CncViewer.ConfigEditor.ViewModels.Interfaces;
using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Links
{
    public abstract class LinkViewModel<T> : VariableViewModel<T>, ILinkViewModel<T>
    {
        public int LinkId { get; private set; }

        public LinkViewModel(int linkId) : base()
        {
            LinkId= linkId;
        }

        protected override void OnSelectableTypeChanged() => RisePropertyChanged(nameof(TargetType));        
    }
}
