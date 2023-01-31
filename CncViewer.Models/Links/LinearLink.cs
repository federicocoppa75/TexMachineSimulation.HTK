using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Models.Links
{
    public abstract class LinearLink : Link
    {
        public double Factor { get; set; }
    }
}
