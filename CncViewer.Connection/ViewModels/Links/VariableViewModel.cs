using CncViewer.Connection.Interfaces.Enums;
using CncViewer.Connection.Interfaces.Links;
using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using MVMIL = Machine.ViewModels.Interfaces.Links;

namespace CncViewer.Connection.ViewModels.Links
{
    public abstract class VariableViewModel : BaseViewModel, IVariable
    {
        public int LinkId { get; set; }
        public VariableType VariableType { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }
        public abstract MVMIL.ILinkViewModel Link { get; set; }

        public abstract void SetValue<V>(V value);
    }

    //public abstract class VariableViewModel<T, TLink> : VariableViewModel, IVariable<T> where TLink : class, MVMIL.ILinkViewModel
    //{
    //    private T _value = default(T);
    //    public T Value 
    //    { 
    //        get => _value;
    //        set
    //        {
    //            if(Set(ref _value, value, nameof(Value)))
    //            {
    //                OnValueChanged();
    //            }
    //        }
    //    }

    //    protected TLink _link;
    //    public override ILinkViewModel Link 
    //    { 
    //        get => _link;
    //        set
    //        {
    //            if(value == null) 
    //            {
    //                _link = null;
    //            }
    //            else if(value is TLink v)
    //            {
    //                Set(ref _link, v, nameof(Link));
    //            }
    //            else
    //            {
    //                throw new InvalidOperationException($"You can not couple {typeof(T).Name} with {typeof(TLink).Name}!");
    //            }
    //        }
    //    }

    //    protected abstract void OnValueChanged();
    //}

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
            if(typeof(T).IsAssignableFrom(typeof(V)))
            {
                (this as VariableViewModel<V>).Value = value;
            }
            else if(value is T tValue)
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

    public abstract class VariableViewModel<T, TLink> : VariableViewModel<T> where TLink : class, MVMIL.ILinkViewModel
    {
        protected TLink _link;
        public override MVMIL.ILinkViewModel Link
        {
            get => _link;
            set
            {
                if (value == null)
                {
                    _link = null;
                }
                else if (value is TLink v)
                {
                    Set(ref _link, v, nameof(Link));
                }
                else
                {
                    throw new InvalidOperationException($"You can not couple {typeof(T).Name} with {typeof(TLink).Name}!");
                }
            }
        }
    }
}
