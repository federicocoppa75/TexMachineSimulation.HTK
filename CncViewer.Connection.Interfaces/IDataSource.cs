using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CncViewer.Connection.Interfaces
{
    public interface IDataSource
    {
        ICommand OpenCommand { get; }
    }
}
