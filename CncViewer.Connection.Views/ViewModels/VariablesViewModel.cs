using CncViewer.Connection.Interfaces;
using CncViewer.Connection.Interfaces.Inputs;
using CncViewer.Connection.Interfaces.Links;
using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVMIoc = Machine.ViewModels.Ioc;

namespace CncViewer.Connection.Views.ViewModels
{
    internal class VariablesViewModel : BaseViewModel
    {
        public IList<IVariable> Variables { get; set; }
        public IList<IVariable> Links => Variables.Where((v) => v is ILink).ToList();
        public IList<IVariable> Inputs => Variables.Where((v) => v is IInput).ToList();


        public VariablesViewModel()
        {
            Variables = MVMIoc.SimpleIoc<IConnectionData>.GetInstance().Variables;
            (Variables as INotifyCollectionChanged).CollectionChanged += OnVariablesCollectionChanged;
        }

        private void OnVariablesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            RisePropertyChanged(nameof(Links));
            RisePropertyChanged(nameof(Inputs));
        }
    }
}
