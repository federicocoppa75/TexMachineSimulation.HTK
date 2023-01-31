using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MVMUI = Machine.ViewModels.UI;
using MDFJ = Machine.DataSource.File.Json;
//using MDFX = Machine.DataSource.File.Xml;
//using MDCR = Machine.DataSource.Client.Rest;
using MVUI = Machine.Views.UI;
using MW32 = Microsoft.Win32;
using M3DGPI = Machine._3D.Geometry.Provider.Interfaces;
using M3DGPIM = Machine._3D.Geometry.Provider.Implementation;
//using MSFM = Machine.StepsSource.File.Msteps;
//using MSFI = Machine.StepsSource.File.Iso;
using MVMI = Machine.ViewModels.Interfaces;
//using MSVMI = Machine.Steps.ViewModels.Interfaces;
using MSVME = Machine.Steps.ViewModels.Extensions;
using MVMB = Machine.ViewModels.Base;
using MRVM3D = MaterialRemove.ViewModels._3D;
using MRVMI = MaterialRemove.ViewModels.Interfaces;
using MVMIF = Machine.ViewModels.Interfaces.Factories;
using MRMB = MaterialRemove.Machine.Bridge;
using MRI = MaterialRemove.Interfaces;
using MVMII = Machine.ViewModels.Interfaces.Insertions;
using MVMIns = Machine.ViewModels.Insertions;
using MVMM = Machine.ViewModels.Messaging;
using MVMBI = Machine.ViewModels.Base.Implementation;
using MVMIoc = Machine.ViewModels.Ioc;
using MVMP = Machine.ViewModels.Probing;
using CVC = CncViewer.Connection;
using CVCI = CncViewer.Connection.Interfaces;
using CVCDSFX = CncViewer.Connection.DataSource.File.Xml;
using CVCB = CncViewer.Connection.Bridge;

namespace CncViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MVMIoc.SimpleIoc<MVMI.IKernelViewModel>.Register<MVMBI.KernelViewModel>();
            MVMIoc.SimpleIoc<MVMM.IMessenger>.Register<MVMM.Messenger>();
            MVMIoc.SimpleIoc<MVMUI.IDataSource>.Register<MDFJ.DataSource>("File.JSON");
            //MVMIoc.SimpleIoc<MVMUI.IDataSource>.Register<MDFX.DataSource>("File.XML");
            //MVMIoc.SimpleIoc<MVMUI.IDataSource>.Register<MDCR.DataSource>("Client.REST");
            MVMIoc.SimpleIoc<MVMUI.IFileDialog>.Register<MVUI.FileDialog<MW32.OpenFileDialog>>("OpenFile");
            MVMIoc.SimpleIoc<MVMUI.IFileDialog>.Register<MVUI.FileDialog<MW32.SaveFileDialog>>("SaveFile");
            MVMIoc.SimpleIoc<MVMUI.IOptionProvider>.Register(new MVMUI.RegisteredOptionProvider<MVMUI.IDataSource>() { Name = "DataSource" });
            MVMIoc.SimpleIoc<M3DGPI.IStreamProvider>.Register<M3DGPIM.StlFileStreamProvider>("File.JSON");
            MVMIoc.SimpleIoc<M3DGPI.IStreamProvider>.Register<M3DGPIM.StlFileStreamProvider>("File.XML");
            MVMIoc.SimpleIoc<M3DGPI.IStreamProvider>.Register<M3DGPIM.RestApiStreamProvider>("Client.REST");
            MVMIoc.SimpleIoc<MVMUI.IListDialog>.Register<MVUI.ListDialog>();
            //MVMIoc.SimpleIoc<MVMUI.IStepsSource>.Register<MSFM.StepsSource>("File.msteps");
            //MVMIoc.SimpleIoc<MVMUI.IStepsSource>.Register<MSFI.StepsSource>("File.iso");
            //MVMIoc.SimpleIoc<MSVMI.IDurationProvider>.Register<MSVME.DurationProvider>();
            //MVMIoc.SimpleIoc<MSVMI.IBackStepActionFactory>.Register<MSVME.BackStepActionFactory>();
            //MVMIoc.SimpleIoc<MSVMI.IActionExecuter>.Register<MSVME.ActionExecuter>();
            MVMIoc.SimpleIoc<MVMUI.IDispatcherHelper>.Register<MVUI.DispatcherHelper>();
            MVMIoc.SimpleIoc<MVMI.Links.ILinkMovementManager>.Register<MSVME.LinkMovementManager>();
            MVMIoc.SimpleIoc<MRVMI.IElementViewModelFactory>.Register<MRVM3D.ElementViewModelFactory>();
            MVMIoc.SimpleIoc<MVMIF.IPanelElementFactory>.Register<MRMB.PanelViewModelFactory>();
            //MVMIoc.SimpleIoc<MVMIF.IPanelElementFactory>.Register<Machine.ViewModels.Factories.PanelViewModelFactory>();
            MVMIoc.SimpleIoc<MVMI.Tools.IToolObserverProvider>.Register<MRMB.ToolsObserverProvider>();
            MVMIoc.SimpleIoc<MRI.IMaterialRemoveData>.Register<MRMB.MaterialRemoveData>();
            MVMIoc.SimpleIoc<MVMII.IInsertionsSinkProvider>.Register<MVMIns.InsertionsSinkProvider>();
            MVMIoc.SimpleIoc<MVMI.Probing.IProbeFactory>.Register<MVMP.ProbeFactory>();
            MVMIoc.SimpleIoc<MVMUI.IApplicationInformationProvider>.Register(new MVMUI.ApplicationInformationProvider(MVMUI.ApplicationType.MachineViewer));
            MVMIoc.SimpleIoc<MVMB.ICommandExceptionObserver>.Register<MVUI.SimpleCommandExceptionObserver>();
            MVMIoc.SimpleIoc<MVMUI.IExceptionObserver>.Register<MVUI.SimpleExceptionObserver>();
            MVMIoc.SimpleIoc<MRI.IPanelExportController>.Register<MRMB.PanelExportController>();
            MVMIoc.SimpleIoc<CVCI.IDataSource>.Register<CVCDSFX.DataSource>();
            var connViewMode = new CVCB.ConnectionViewModel();
            MVMIoc.SimpleIoc<CVCI.IConnectionData>.Register(connViewMode);
            MVMIoc.SimpleIoc<CVCI.IConnectionManager>.Register(connViewMode);
            //MVMIoc.SimpleIoc<MVMI.IProcessCaller>.Register<CVC.TimedProcessCaller>("TEX");
            //MVMIoc.SimpleIoc<MVMI.Links.ILinkMovementManager>.Register<CVC.LinkMovementManagerDummy>();
            MVMIoc.SimpleIoc<MVMI.IProgressState>.Register<CVC.DummyProgressState>();
        }
    }
}
