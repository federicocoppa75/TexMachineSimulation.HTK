using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MVMIoc = Machine.ViewModels.Ioc;
using MVMI = Machine.ViewModels.Interfaces;
using MVMBI = Machine.ViewModels.Base.Implementation;
using MVMM = Machine.ViewModels.Messaging;
using MVUI = Machine.Views.UI;
using MW32 = Microsoft.Win32;
using MVMUI = Machine.ViewModels.UI;
using MDFJ = Machine.DataSource.File.Json;

namespace CncViewer.ConfigEditor
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
            MVMIoc.SimpleIoc<MVMUI.IFileDialog>.Register<MVUI.FileDialog<MW32.OpenFileDialog>>("OpenFile");
            MVMIoc.SimpleIoc<MVMUI.IFileDialog>.Register<MVUI.FileDialog<MW32.SaveFileDialog>>("SaveFile");
            MVMIoc.SimpleIoc<MVMUI.IOptionProvider>.Register(new MVMUI.RegisteredOptionProvider<MVMUI.IDataSource>() { Name = "DataSource" });
            MVMIoc.SimpleIoc<MVMUI.IApplicationInformationProvider>.Register(new MVMUI.ApplicationInformationProvider(MVMUI.ApplicationType.MachineViewer));
        }
    }
}
