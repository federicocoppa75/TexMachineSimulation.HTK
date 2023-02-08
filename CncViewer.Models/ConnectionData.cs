using CncViewer.Models.Inputs;
using CncViewer.Models.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Models
{
    public class ConnectionData
    {
        public List<Link> Links { get; set; } = new List<Link>();
        public List<Input> Inputs { get; set; } = new List<Input>();
    }
}
