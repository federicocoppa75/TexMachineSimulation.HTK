using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CncViewer.Connection.Interfaces
{
    public interface IConnectionManager
    {
        int SampleTime { get; set; }
        string IpAddress { get; set; }
        ICommand ConnectCommand { get; }
        ICommand DisconnectCommand { get; }
        ICommand UnloadVariablesCommand { get; }
    }
}
