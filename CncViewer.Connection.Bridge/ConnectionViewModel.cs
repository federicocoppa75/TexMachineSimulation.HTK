using CncViewer.Connection.Interfaces;
using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Input;
using CVC = CncViewer.Connection;
using GTCI = GZSoft.Tex.Controller.Interface;
using GTC = GZSoft.Tex.Controller;
using GTCC = GZSoft.Tex.Controller.Communication;
using System.Collections.Specialized;
using MVMI = Machine.ViewModels.Interfaces;
using CncViewer.Connection.Interfaces.Links;
using System.Threading;
using System.Threading.Tasks;
using MVMUI = Machine.ViewModels.UI;
using CncViewer.Connection.Interfaces.Inputs;

namespace CncViewer.Connection.Bridge
{
    public class ConnectionViewModel : CVC.ViewModels.ConnectionViewModel, IConnectionManager
    {
        private int _processing;
        private DateTime _lastReadTime;
        private bool _firstRead;
        private bool _stopRequested;

        protected GTCI.IController _controller;

        public int SampleTime { get; set; } = 300;
        public string IpAddress { get; set; } = "192.168.0.200";
        public bool IsConnected => _controller != null && _controller.IsConnect;

        private ICommand _unloadVariablesCommand;
        public ICommand UnloadVariablesCommand => _unloadVariablesCommand ?? (_unloadVariablesCommand = new RelayCommand(() => UnloadVariablesCommandImpl(), () => (Variables.Count > 0 && !IsConnected)));

        private ICommand _connectCommand;
        public ICommand ConnectCommand => _connectCommand ?? (_connectCommand = new RelayCommand(() => ConnectCommandImpl(), () => (Variables.Count > 0 && !IsConnected)));

        private ICommand _disconnectCommand;
        public ICommand DisconnectCommand => _disconnectCommand ?? (_disconnectCommand = new RelayCommand(() => DisconnectCommandImpl(), () => (IsConnected && !_stopRequested)));

        private void UnloadVariablesCommandImpl()
        {
            Variables.Clear();
            _variableToWrite.Clear();
        }

        private void ConnectCommandImpl()
        {
            if(IPAddress.TryParse(IpAddress, out var address)) 
            {
                _controller = GTC.ControllerDetectionHelper.CreateInstance(new GTCC.EthernetSocket(address));

                if(_controller != null && _controller.IsConnect)
                {
                    // Set password universale
                    ConnectionHelper.SendFrame(_controller, "AQU" + "0546");

                    UpdateCommands();
                    Start();
                }
            }
        }

        private void DisconnectCommandImpl()
        {
            _stopRequested = true;
            UpdateCommands();
        }

        private void ExecuteStopAndDisconnect()
        {
            var processCaller = GetInstance<MVMI.IProcessCaller>();
            
            processCaller.ProcessRequest -= OnProcessRequest;

            // Reset password universale
            ConnectionHelper.SendFrame(_controller, "AQU");

            _controller.Disconnect();
            _controller = null;

            UpdateCommands();
        }

        private void UpdateCommands()
        {
            GetInstance<MVMUI.IDispatcherHelper>().CheckBeginInvokeOnUi(() =>
            {
                (_connectCommand as RelayCommand).RaiseCanExecuteChanged();
                (_disconnectCommand as RelayCommand).RaiseCanExecuteChanged();
                (_unloadVariablesCommand as RelayCommand).RaiseCanExecuteChanged();
            });
        }

        protected override void OnVariablesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnVariablesCollectionChanged(sender, e);

            if(IsConnected) 
            {
                DisconnectCommandImpl();
            }
            else
            {
                UpdateCommands();
            }            
        }

        private void Start()
        {
            var processCaller = GetInstance<MVMI.IProcessCaller>();

            _firstRead = true;
            processCaller.ProcessRequest += OnProcessRequest;
        }

        private void OnProcessRequest(object sender, DateTime e)
        {
            if (Interlocked.CompareExchange(ref _processing, 1, 0) == 0)
            {
                Task.Run(() => 
                {
                    if (_firstRead)
                    {
                        _firstRead = false;
                        _lastReadTime = e;
                        ReadWriteVariables();
                    }
                    else
                    {
                        var dt = e - _lastReadTime;

                        if (dt.TotalMilliseconds >= SampleTime)
                        {
                            _lastReadTime = e;
                            ReadWriteVariables();
                        }
                    }

                    Interlocked.Exchange(ref _processing, 0);

                    if (_stopRequested) ExecuteStopAndDisconnect();
                });
            }
        }

        protected bool ReadWriteVariables()
        {
            if(ReadVariables() && WriteVariables())
            {
                return true;
            }
            else
            {
                _controller = null;
                UpdateCommands();
                return false;
            }
        }

        protected virtual bool ReadVariables()
        {
            foreach (var variable in Variables)
            {
                if (variable.Index >= 0)
                {
                    if(!Read(variable)) return false;
                }
            }

            return true;
        }

        private bool Read(IVariable variable)
        {
            if (!CheckConnection()) return false;

            switch (variable.VariableType)
            {
                case Interfaces.Enums.VariableType.Flag:
                    variable.SetValue(ConnectionHelper.GetBitF(_controller, GTC.SubSystem.PLC, variable.Index));
                    break;
                case Interfaces.Enums.VariableType.Out:
                    variable.SetValue(ConnectionHelper.GetBitO(_controller, GTC.SubSystem.PLC, variable.Index));
                    break;
                case Interfaces.Enums.VariableType.Word:
                    variable.SetValue(ConnectionHelper.GetVariableW(_controller, GTC.SubSystem.PLC, variable.Index));
                    break;
                case Interfaces.Enums.VariableType.DWord:
                    variable.SetValue((float)ConnectionHelper.GetVariableV(_controller, GTC.SubSystem.PLC, variable.Index));
                    break;
                default:
                    throw new NotImplementedException($"Read variable for type {variable.VariableType} not implemented!");
            }

            return true;
        }

        protected virtual bool WriteVariables() 
        {
            while(!_variableToWrite.IsEmpty)
            {
                if(_variableToWrite.TryTake(out var variable)) 
                {
                    if(!Write(variable)) return false;
                }
            }

            return true;
        }

        private bool Write(IVariable variable) 
        {
            if (!CheckConnection()) return false;

            switch (variable.VariableType)
            {
                case Interfaces.Enums.VariableType.Flag:
                    ConnectionHelper.SetBitF(_controller, GTC.SubSystem.PLC, variable.Index, (variable as IInput<bool>).RequestValue);
                    break;
                case Interfaces.Enums.VariableType.Out:
                case Interfaces.Enums.VariableType.Word:
                case Interfaces.Enums.VariableType.DWord:
                default:
                    throw new NotImplementedException($"Write variable for type {variable.VariableType} not implemented!");
            }

            return true;
        }

        protected bool CheckConnection() =>  (_controller != null) && _controller.IsConnect;
    }
}
