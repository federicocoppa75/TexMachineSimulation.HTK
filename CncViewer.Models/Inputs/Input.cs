using CncViewer.Models.Enums;
using CncViewer.Models.Links;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CncViewer.Models.Inputs
{
    [XmlInclude(typeof(BinaryInput))]
    public class Input
    {
        public VariableType VariableType { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }
    }
}
