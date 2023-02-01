using GZSoft.Tex.Controller.Variable;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection.Bridge.ViewModels.Links
{
    public class FixedPointVariableViewModel : Connection.ViewModels.Links.FixedPointVariableViewModel
    {
        public override void SetValue<V>(V value)
        {
            if(value is FixedPoint fp)
            {
                base.SetValue((float)fp);
            }
            else
            {
                base.SetValue(value);
            }            
        }
    }
}
