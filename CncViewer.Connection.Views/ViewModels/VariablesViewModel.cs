using CncViewer.Connection.Interfaces;
using CncViewer.Connection.Interfaces.Links;
using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVMIoc = Machine.ViewModels.Ioc;

namespace CncViewer.Connection.Views.ViewModels
{
    internal class VariablesViewModel : BaseViewModel
    {
        public IList<IVariable> Variables { get; set; }

        public VariablesViewModel()
        {
            Variables = MVMIoc.SimpleIoc<IConnectionData>.GetInstance().Variables;
        }
    }
}
