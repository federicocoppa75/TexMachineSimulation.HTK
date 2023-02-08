using CncViewer.ConfigEditor.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.DataSource.File.Xml
{
    public interface IConfigProvider
    {
        IList<ILinkViewModel> Links { get; }
        IList<IInputViewModel> Inputs { get; }
    }
}
