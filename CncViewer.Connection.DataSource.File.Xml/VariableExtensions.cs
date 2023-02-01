using CncViewer.Connection.Interfaces.Links;
using CncViewer.Models.Links;
using System;
using System.Collections.Generic;
using System.Text;
using MVMM = Machine.ViewModels.Messaging;
using MVMIoc = Machine.ViewModels.Ioc;
using CncViewer.Connection.Interfaces.Enums;
using CncViewer.Connection.Interfaces;

namespace CncViewer.Connection.DataSource.File.Xml
{
    internal static class VariableExtensions
    {
        private static IVariableViewModelFacotry Factory => MVMIoc.SimpleIoc<IVariableViewModelFacotry>.GetInstance();

        public static IVariable ToVariable(this Link link)
        {
            switch (link.VariableType)
            {
                case Models.Enums.VariableType.Flag:
                    return Factory.Create(VariableType.Flag, link.Index, link.LinkId, link.Description);
                case Models.Enums.VariableType.Out:
                    return Factory.Create(VariableType.Out, link.Index, link.LinkId, link.Description);
                case Models.Enums.VariableType.Word:
                    return Factory.Create(VariableType.Word, link.Index, link.LinkId, link.Description, (link as WordLink).Factor);
                case Models.Enums.VariableType.DWord:
                    return Factory.Create(VariableType.DWord, link.Index, link.LinkId, link.Description, (link as DWordLink).Factor);
                default:
                    throw new NotImplementedException($"Conversion from type {link.VariableType} not implemented!");
            }
        }
    }
}
