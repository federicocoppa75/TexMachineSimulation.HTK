using CncViewer.Connection.Interfaces;
using CncViewer.Connection.Interfaces.Links;
using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CncViewer.Connection.ViewModels
{
    public class ConnectionViewModel : BaseViewModel, IConnectionData
    {
        public IList<IVariable> Variables {get; private set;}

        public ConnectionViewModel()
        {
            var collection = new ObservableCollection<IVariable>();

            collection.CollectionChanged += OnVariablesCollectionChanged;

            Variables = collection;
        }

        protected virtual void OnVariablesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {            
        }
    }
}
