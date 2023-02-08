using CncViewer.Connection.Interfaces.Links;
using System;
using System.Collections.Generic;
using System.Text;
using MVMIL = Machine.ViewModels.Interfaces.Links;

namespace CncViewer.Connection.ViewModels.Links
{
    public abstract class LinkViewModel<T, TLink> : VariableViewModel<T>, ILink<T>, ILink where TLink : class, MVMIL.ILinkViewModel
    {
        public int LinkId { get; set; }

        protected TLink _link;
        public MVMIL.ILinkViewModel Link
        {
            get => _link;
            set
            {
                if (value == null)
                {
                    _link = null;
                }
                else if (value is TLink v)
                {
                    Set(ref _link, v, nameof(Link));
                }
                else
                {
                    throw new InvalidOperationException($"You can not couple {typeof(T).Name} with {typeof(TLink).Name}!");
                }
            }
        }
    }
}
