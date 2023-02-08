﻿using CncViewer.Connection.Interfaces;
using CncViewer.Connection.Interfaces.Links;
using CncViewer.Connection.Messages;
using Machine.ViewModels.Base;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CncViewer.Connection.ViewModels
{
    public class ConnectionViewModel : BaseViewModel, IConnectionData
    {
        protected ConcurrentBag<IVariable> _variableToWrite;

        public IList<IVariable> Variables {get; private set;}

        public ConnectionViewModel()
        {
            _variableToWrite= new ConcurrentBag<IVariable>();
            var collection = new ObservableCollection<IVariable>();

            collection.CollectionChanged += OnVariablesCollectionChanged;

            Variables = collection;

            Messenger.Register<AddVariableToWriteMessage>(this, OnAddVariableToWriteMessage);
        }

        private void OnAddVariableToWriteMessage(AddVariableToWriteMessage msg)
        {
            _variableToWrite.Add(msg.Variable);
        }

        protected virtual void OnVariablesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {            
        }
    }
}
