using CncViewer.Connection.Interfaces;
using CncViewer.Connection.Interfaces.Enums;
using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.ViewModels
{
    public abstract class VariableViewModel : BaseViewModel, IVariable
    {
        public VariableType VariableType { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }

        public abstract void SetValue<V>(V value);
    }

    public abstract class VariableViewModel<T> : VariableViewModel, IVariable<T>
    {
        private T _value = default(T);
        public T Value
        {
            get => _value;
            set
            {
                if (Set(ref _value, value, nameof(Value)))
                {
                    OnValueChanged();
                }
            }
        }

        public override void SetValue<V>(V value)
        {
            if (typeof(T).IsAssignableFrom(typeof(V)))
            {
                (this as VariableViewModel<V>).Value = value;
            }
            else if (value is T tValue)
            {
                Value = tValue;
            }
            else
            {
                throw new InvalidOperationException($"Can not assign a value of type {typeof(V)} to instance of type {typeof(T)}!");
            }
        }

        protected abstract void OnValueChanged();
    }

}
