using CncViewer.Connection.Interfaces;
using CncViewer.Connection.Interfaces.Enums;
using CncViewer.Connection.Interfaces.Links;
using System;
using System.Collections.Generic;
using System.Text;
using CVCVML = CncViewer.Connection.ViewModels.Links;
using CVCBVML = CncViewer.Connection.Bridge.ViewModels.Links;
using MVMIL = Machine.ViewModels.Interfaces.Links;
using MVMIoc = Machine.ViewModels.Ioc;
using MVMM = Machine.ViewModels.Messaging;
using MVMML = Machine.ViewModels.Messages.Links;


namespace CncViewer.Connection.Bridge.Factories
{
    public class VariableViewModelFacotry : IVariableViewModelFacotry
    {
        public IVariable Create(VariableType type, int index, int linkId, string description, double factor = 0)
        {
            switch (type)
            {
                case VariableType.Flag:
                case VariableType.Out:
                    return new CVCVML.BinaryVariableViewModel() { VariableType = type, Index = index, LinkId = linkId, Description = description, Link = GetLink(linkId) };
                case VariableType.Word:
                    return new CVCVML.LinearLinkViewModel() { VariableType = type, Index = index, LinkId = linkId, Description = description, Factor = factor, Link = GetLink(linkId) };
                case VariableType.DWord:
                    return new CVCBVML.FixedPointVariableViewModel() {VariableType = type, Index = index, LinkId = linkId, Description = description, Factor = factor, Link = GetLink(linkId) };
                default:
                    throw new NotImplementedException($"The type {type} of variable is not implemented!");
            }
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
