using System;
using System.Collections.Generic;
using System.Text;
using CVCEVM = CncViewer.ConfigEditor.ViewModels;
using CVCEVML = CncViewer.ConfigEditor.ViewModels.Links;
using CVCEVMI = CncViewer.ConfigEditor.ViewModels.Interfaces;
using CVM = CncViewer.Models;
using CVML = CncViewer.Models.Links;
using CVME = CncViewer.Models.Enums;
using System.Threading;
using CVMI = CncViewer.Models.Inputs;
using CncViewer.ConfigEditor.ViewModels.Inputs;

namespace CncViewer.ConfigEditor.DataSource.File.Xml
{
    internal static class ViewModelsExtensions
    {
        public static CVM.ConnectionData ToModel(this CVCEVM.ConnectionViewModel vm)
        {
            var data = new CVM.ConnectionData();

            foreach (var link in vm.Links)
            {
                data.Links.Add(link.ToModel());
            }

            foreach (var input in vm.Inputs)
            {
                data.Inputs.Add(input.ToModel());
            }

            return data;
        }

        public static CVM.ConnectionData ToModel(this IConfigProvider configProvider)
        {
            var data = new CVM.ConnectionData();

            foreach (var link in configProvider.Links)
            {
                data.Links.Add(link.ToModel());
            }

            foreach (var input in configProvider.Inputs)
            {
                data.Inputs.Add(input.ToModel());
            }

            return data;
        }

        public static void LoadViewModels(this IConfigProvider configProvider, CVM.ConnectionData source)
        {
            configProvider.Links.Clear();
            configProvider.Inputs.Clear();

            foreach (var link in source.Links)
            {
                configProvider.Links.Add(link.ToViewModel());
            }

            foreach (var input in source.Inputs)
            {
                configProvider.Inputs.Add(input.ToViewModel());
            }
        }

        public static CVMI.Input ToModel(this CVCEVMI.IInputViewModel vm)
        {
            if (vm is CVCEVMI.IBinaryInputViewModel bvm) return ToModel(bvm);
            else throw new NotImplementedException($"Conversion to model of {vm.GetType().FullName} is not implemented!");
        }

        public static CVML.Link ToModel(this CVCEVMI.ILinkViewModel vm)
        {
            if (vm is CVCEVMI.IBinaryLinkViewModel bvm) return ToModel(bvm);
            else if (vm is CVCEVMI.ILinearLinkViewModel lvm) return ToModel(lvm);
            else throw new NotImplementedException($"Conversion to model of {vm.GetType().FullName} is not implemented!");
        }

        public static CVCEVM.ConnectionViewModel ToViewModel(this CVM.ConnectionData data)
        {
            var vm = new CVCEVM.ConnectionViewModel();

            foreach (var item in data.Links)
            {
                vm.Links.Add(item.ToViewModel());
            }

            return vm;
        }

        public static CVCEVMI.IInputViewModel ToViewModel(this CVMI.Input input)
        {
            if (input is CVMI.BinaryInput bi) return ToViewModel(bi);
            else throw new NotImplementedException($"Conversion to view model not implemented for {input.VariableType} input!");
        }

        private static CVCEVMI.IInputViewModel ToViewModel(this CVMI.BinaryInput input)
        {
            var vm = new BinaryInputViewModel()
            {
                Index = input.Index,
                Description= input.Description,
                SelectableType = CVCEVM.Enums.BinaryLinkTarget.Flag,
            };

            switch (input.BinaryInputType)
            {
                case CVME.BinaryInputType.Pulse:
                    vm.BinaryInputType = CVCEVM.Enums.BinaryInputType.Pulse;
                    break;
                case CVME.BinaryInputType.Flag:
                    vm.BinaryInputType = CVCEVM.Enums.BinaryInputType.Flag;
                    break;
                default:
                    throw new NotImplementedException($"Conversion to view model not implemented for {nameof(BinaryInputViewModel)}({input.BinaryInputType}) input type!");
            }

            return vm;
        }

        public static CVCEVMI.ILinkViewModel ToViewModel(this CVML.Link link)
        {
            CVCEVMI.ILinkViewModel vm = null;

            switch (link.VariableType)
            {
                case CVME.VariableType.Flag:
                    vm = new CVCEVML.BinaryLinkViewModel(link.LinkId) { SelectableType = CVCEVM.Enums.BinaryLinkTarget.Flag };
                    break;
                case CVME.VariableType.Out:
                    vm = new CVCEVML.BinaryLinkViewModel(link.LinkId) { SelectableType = CVCEVM.Enums.BinaryLinkTarget.Out };
                    break;
                case CVME.VariableType.Word:
                    vm = new CVCEVML.LinearLinkViewModel(link.LinkId) { Factor = (link as CVML.LinearLink).Factor, SelectableType = CVCEVM.Enums.LinearLinkTarget.Word };
                    break;
                case CVME.VariableType.DWord:
                    vm = new CVCEVML.LinearLinkViewModel(link.LinkId) { Factor = (link as CVML.LinearLink).Factor, SelectableType = CVCEVM.Enums.LinearLinkTarget.DWord };
                    break;
                default: throw new NotImplementedException($"Conversion to view model not implemented for {link.VariableType} link!");
            }

            TransferProps(link, vm);

            return vm;
        }

        public static CVMI.Input ToModel(CVCEVMI.IBinaryInputViewModel vm)
        {
            var m =  new CVMI.BinaryInput()
            {
                VariableType = CVME.VariableType.Flag,
                Index= vm.Index,
                Description= vm.Description,
            };

            switch (vm.BinaryInputType)
            {
                case CVCEVM.Enums.BinaryInputType.Pulse:
                    m.BinaryInputType = CVME.BinaryInputType.Pulse;
                    break;
                case CVCEVM.Enums.BinaryInputType.Flag:
                    m.BinaryInputType= CVME.BinaryInputType.Flag;
                    break;
                default:
                    break;
            }

            return m;
        }

        public static CVML.Link ToModel(CVCEVMI.IBinaryLinkViewModel vm)
        {
            CVML.Link link = null;

            switch (vm.TargetType)
            {
                case CVCEVM.Enums.TargetType.Out:
                    link = new CVML.OutputLink() { VariableType = CVME.VariableType.Out };
                    break;
                case CVCEVM.Enums.TargetType.Flag:
                    link = new CVML.FlagLink() { VariableType = CVME.VariableType.Flag };
                    break;
                default: throw new NotImplementedException($"Binary link conversion of {vm.TargetType} is not implemented!");
            }

            TransferProps(vm, link);

            return link;
        }

        public static CVML.Link ToModel(CVCEVMI.ILinearLinkViewModel vm)
        {
            //bool idDWord = (vm.LinearTargetType == CVCEVM.Enums.LinearLinkTarget.DWord);
            CVML.LinearLink link = null;

            switch (vm.TargetType)
            {
                case CVCEVM.Enums.TargetType.Word:
                    link = new CVML.WordLink() { VariableType = CVME.VariableType.Word, Factor = vm.Factor };
                    break;
                case CVCEVM.Enums.TargetType.DWord:
                    link = new CVML.DWordLink() { VariableType = CVME.VariableType.DWord, Factor = vm.Factor };
                    break;
                default:
                    throw new NotImplementedException($"Linear link conversion of {vm.TargetType} not implemented!");
            }

            TransferProps(vm, link);

            return link;
        }

        public static void TransferProps(CVCEVMI.ILinkViewModel source, CVML.Link dest)
        {
            dest.LinkId = source.LinkId;
            dest.Index = source.Index;
            dest.Description = source.Description;
        }

        public static void TransferProps(CVML.Link source, CVCEVMI.ILinkViewModel dest)
        {
            dest.Index = source.Index;
            dest.Description = source.Description;
        }
    }
}
