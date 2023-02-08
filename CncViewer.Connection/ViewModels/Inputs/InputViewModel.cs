using CncViewer.Connection.Interfaces.Inputs;
using CncViewer.Connection.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.ViewModels.Inputs
{
    public abstract class InputViewModel<T> : VariableViewModel<T>, IInput<T>, IInput
    {
        private T _requestValue;
        public T RequestValue 
        { 
            get => _requestValue; 
            set
            {
                if(Set(ref _requestValue, value, nameof(RequestValue)))
                {
                    OnRequestValueChanged();
                }
            }
        }

        protected virtual void OnRequestValueChanged() 
        {
            Messenger.Send(new AddVariableToWriteMessage() { Variable = this });
        }
    }
}
