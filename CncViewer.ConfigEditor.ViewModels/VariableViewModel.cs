using CncViewer.ConfigEditor.ViewModels.Enums;
using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels
{
    public abstract class VariableViewModel : BaseViewModel
    {
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
    }

    public abstract class VariableViewModel<T> : VariableViewModel
    {
        private T _selectableType;
        public T SelectableType 
        { 
            get => _selectableType; 
            set
            {
                if(Set(ref _selectableType, value, nameof(SelectableType))) 
                {
                    OnSelectableTypeChanged();
                }
            }
        }

        protected abstract void OnSelectableTypeChanged();
    }
}
