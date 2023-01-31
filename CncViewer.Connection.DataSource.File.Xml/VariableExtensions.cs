using CncViewer.Connection.Interfaces.Links;
using CncViewer.Connection.ViewModels.Links;
using CncViewer.Models.Links;
using System;
using System.Collections.Generic;
using System.Text;
using MVMIL = Machine.ViewModels.Interfaces.Links;
using MVMM = Machine.ViewModels.Messaging;
using MVMIoc = Machine.ViewModels.Ioc;
using MVMML = Machine.ViewModels.Messages.Links;

namespace CncViewer.Connection.DataSource.File.Xml
{
    internal static class VariableExtensions
    {
        public static IVariable ToVariable(this Link link)
        {
            if (link is WordLink wl) return ToVariable(wl);
            if (link is DWordLink dwl) return ToVariable(dwl);
            else if(link is OutputLink ol) return ToVariable(ol);
            else if(link is FlagLink fl) return ToVariable(fl);
            else throw new NotImplementedException($"Conversion from type {link.VariableType} not implemented!");
        }

        static IVariable ToVariable(this WordLink link)
        {
            var v = new LinearLinkViewModel() { VariableType = Interfaces.Enums.VariableType.Word, Factor = link.Factor };

            UpdateVariable(link, v);

            return v;
        }

        static IVariable ToVariable(this DWordLink link)
        {
            var v = new FixedPointVariableViewModel() { VariableType = Interfaces.Enums.VariableType.DWord, Factor = link.Factor };

            UpdateVariable(link, v);

            return v;
        }

        static IVariable ToVariable(this OutputLink link)
        {
            var v = new BinaryVariableViewModel() { VariableType = Interfaces.Enums.VariableType.Out };

            UpdateVariable(link, v);

            return v;
        }

        static IVariable ToVariable(this FlagLink link)
        {
            var v = new BinaryVariableViewModel() { VariableType = Interfaces.Enums.VariableType.Flag };

            UpdateVariable(link, v);

            return v;
        }

        static void UpdateVariable(Link link, VariableViewModel vvm)
        {
            vvm.LinkId = link.LinkId;
            vvm.Index= link.Index;
            vvm.Description= link.Description;
            vvm.Link = GetLink(link.LinkId);
        }

        static MVMIL.ILinkViewModel GetLink(int id)
        {
            MVMIL.ILinkViewModel lvm = null;

            MVMIoc.SimpleIoc<MVMM.IMessenger>.GetInstance().Send(new MVMML.GetLinkMessage()
            {
                Id = id,
                SetLink = (link) => lvm = link
            });

            return lvm;
        }
    }
}
