using CncViewer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CncViewer.Models.Links
{
    [XmlInclude(typeof(WordLink))]
    [XmlInclude(typeof(DWordLink))]
    [XmlInclude(typeof(OutputLink))]
    [XmlInclude(typeof(FlagLink))]
    public abstract class Link
    {
        public int LinkId { get; set; }
        public VariableType VariableType { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }
    }
}
