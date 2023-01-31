using CncViewer.ConfigEditor.ViewModels.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.ConfigEditor.ViewModels
{
    public class ConnectionViewModel
    {
        public List<LinkViewModel> Links { get; set; } = new List<LinkViewModel>();
    }
}
