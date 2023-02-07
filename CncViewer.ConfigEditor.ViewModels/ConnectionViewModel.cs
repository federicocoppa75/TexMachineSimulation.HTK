using CncViewer.ConfigEditor.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels
{
    public class ConnectionViewModel
    {
        public List<ILinkViewModel> Links { get; set; } = new List<ILinkViewModel>();
    }
}
