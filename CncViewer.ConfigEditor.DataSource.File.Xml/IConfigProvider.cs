using CncViewer.ConfigEditor.ViewModels.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.DataSource.File.Xml
{
    public interface IConfigProvider
    {
        IList<LinkViewModel> Links { get; }
    }
}
