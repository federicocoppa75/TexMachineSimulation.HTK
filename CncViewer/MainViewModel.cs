using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVM = Machine.ViewModels;
using M3DVE = Machine._3D.Views.Enums;
using VMUI = Machine.ViewModels.UI;
using M3DVI = Machine._3D.Views.Interfaces;
using MRI = MaterialRemove.Interfaces;
using MRIE = MaterialRemove.Interfaces.Enums;
using MVMIoc = Machine.ViewModels.Ioc;
using CVCI = CncViewer.Connection.Interfaces;
using MVMIL = Machine.ViewModels.Interfaces.Links;

namespace CncViewer
{
    internal class MainViewModel : MVM.MainViewModel
    {
        public M3DVI.IBackgroundColor BackgroundColor => MVMIoc.SimpleIoc<M3DVI.IBackgroundColor>.GetInstance();
        public VMUI.IOptionProvider<M3DVE.LightType> LightType => MVMIoc.SimpleIoc<VMUI.IOptionProvider<M3DVE.LightType>>.GetInstance();
        public ICollection<VMUI.IFlag> View3DFlags => MVMIoc.SimpleIoc<VMUI.IPeropertiesProvider>.GetInstance().Flags;
        public ICollection<VMUI.IOptionProvider> View3DOptions => MVMIoc.SimpleIoc<VMUI.IPeropertiesProvider>.GetInstance().Options;
        public VMUI.IOptionProvider DataSource => MVMIoc.SimpleIoc<VMUI.IOptionProvider>.GetInstance();
        public MRI.IMaterialRemoveData MaterialRemoveData => MVMIoc.SimpleIoc<MRI.IMaterialRemoveData>.GetInstance();
        public VMUI.IOptionProvider<MRIE.PanelFragment> PanelFragmentOptions => MVMIoc.SimpleIoc<VMUI.IOptionProvider<MRIE.PanelFragment>>.GetInstance();
        public VMUI.IOptionProvider<MRIE.SectionDivision> SectionDivisionOptions => MVMIoc.SimpleIoc<VMUI.IOptionProvider<MRIE.SectionDivision>>.GetInstance();
        public VMUI.IProbesController ProbesController => MVMIoc.SimpleIoc<VMUI.IProbesController>.GetInstance();
        public VMUI.IOptionProvider<M3DVE.ProbeSize> ProbeSize => MVMIoc.SimpleIoc<VMUI.IOptionProvider<M3DVE.ProbeSize>>.GetInstance();
        public VMUI.IOptionProvider<M3DVE.ProbeColor> ProbeColor => MVMIoc.SimpleIoc<VMUI.IOptionProvider<M3DVE.ProbeColor>>.GetInstance();
        public VMUI.IOptionProvider<M3DVE.ProbeShape> ProbeShape => MVMIoc.SimpleIoc<VMUI.IOptionProvider<M3DVE.ProbeShape>>.GetInstance();
        public MRI.IPanelExportController PanelController => MVMIoc.SimpleIoc<MRI.IPanelExportController>.GetInstance();
        public VMUI.IOptionProvider<string> PanelOuterMaterial => MVMIoc.SimpleIoc<VMUI.IOptionProvider<string>>.GetInstance("PanelOuterMaterial");
        public VMUI.IOptionProvider<string> PanelInnerMaterial => MVMIoc.SimpleIoc<VMUI.IOptionProvider<string>>.GetInstance("PanelInnerMaterial");
        public VMUI.IViewExportController ViewExportController => MVMIoc.SimpleIoc<VMUI.IViewExportController>.GetInstance();
        public CVCI.IDataSource ConnectionDataSource => MVMIoc.SimpleIoc<CVCI.IDataSource>.GetInstance();
        public CVCI.IConnectionManager ConnectionManager => MVMIoc.SimpleIoc<CVCI.IConnectionManager>.GetInstance();
        public MVMIL.ILinkMovementController LinkMovementController => MVMIoc.SimpleIoc<MVMIL.ILinkMovementManager>.GetInstance();

        public MainViewModel() : base()
        {
            MVMIoc.SimpleIoc<VMUI.IOptionProvider<MRIE.PanelFragment>>
                .Register(new VMUI.EnumOptionProxy<MRIE.PanelFragment>(() => MaterialRemoveData.PanelFragment,
                                                                  (v) => MaterialRemoveData.PanelFragment = v));

            MVMIoc.SimpleIoc<VMUI.IOptionProvider<MRIE.SectionDivision>>
                .Register(new VMUI.EnumOptionProxy<MRIE.SectionDivision>(() => MaterialRemoveData.SectionDivision,
                                                                    (v) => MaterialRemoveData.SectionDivision = v));

        }
    }
}
