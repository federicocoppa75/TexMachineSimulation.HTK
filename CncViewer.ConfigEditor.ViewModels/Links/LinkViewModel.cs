using CncViewer.ConfigEditor.ViewModels.Enums;
using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels.Links
{
    public abstract class LinkViewModel : BaseViewModel
    {
        public int LinkId { get; private set; }

        public abstract TargetType TargetType { get; }

        private int _index = -1;
        public int Index 
        { 
            get => _index;
            set => Set(ref _index, value, nameof(Index)); 
        }

        private string _description;
        public string Description 
        { 
            get => _description; 
            set => Set(ref _description, value, nameof(Description)); 
        }

        public LinkViewModel(int linkId) : base()
        {
            LinkId= linkId;
        }
    }
}
