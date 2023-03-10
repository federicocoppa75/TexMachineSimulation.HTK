using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MVUI = Machine.Views.UI;
using MVH = Machine.Views.Helpers;

namespace CncViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            MVUI.DispatcherHelper.Initialize();

            UpdateFromSettings();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var cmd = (DataContext as MainViewModel).ConnectionManager.DisconnectCommand;

            if ((cmd != null) && cmd.CanExecute(null)) cmd.Execute(null);

            base.OnClosing(e);

            SaveToSettings();
            Properties.Settings.Default.Save();
        }

        private void SaveToSettings()
        {
            var vm = DataContext as MainViewModel;

            Properties.Settings.Default.BackgroundColorStart = MVH.MainWindowHelper.Convert(vm.BackgroundColor.Start);
            Properties.Settings.Default.BackgroundColorStop = MVH.MainWindowHelper.Convert(vm.BackgroundColor.Stop);
            Properties.Settings.Default.LightType = vm.LightType.Value.ToString();
            Properties.Settings.Default.View3DFlags = MVH.MainWindowHelper.Convert(vm.View3DFlags);
            Properties.Settings.Default.View3DOptions = MVH.MainWindowHelper.Convert(vm.View3DOptions);
            Properties.Settings.Default.DataSource = vm.DataSource.ToString();
            Properties.Settings.Default.MaterialRemove = vm.MaterialRemoveData.Enable;
            Properties.Settings.Default.ProbeSize = vm.ProbeSize.Value.ToString();
            Properties.Settings.Default.ProbeColor = vm.ProbeColor.Value.ToString();
            Properties.Settings.Default.ProbeShape = vm.ProbeShape.Value.ToString();
            Properties.Settings.Default.PanelOuterMaterial = vm.PanelOuterMaterial.Value;
            Properties.Settings.Default.PanelInnerMaterial = vm.PanelInnerMaterial.Value;
            Properties.Settings.Default.PanelFragmentType = vm.MaterialRemoveData.PanelFragment.ToString();
            Properties.Settings.Default.SectionDivision = vm.MaterialRemoveData.SectionDivision.ToString();
            Properties.Settings.Default.DynamicTransition = vm.LinkMovementController.Enable;
            Properties.Settings.Default.IpAddress = vm.ConnectionManager.IpAddress;
            Properties.Settings.Default.SampleTime= vm.ConnectionManager.SampleTime;
        }

        private void UpdateFromSettings()
        {
            var vm = DataContext as MainViewModel;

            vm.BackgroundColor.Start = MVH.MainWindowHelper.Convert(Properties.Settings.Default.BackgroundColorStart);
            vm.BackgroundColor.Stop = MVH.MainWindowHelper.Convert(Properties.Settings.Default.BackgroundColorStop);
            vm.LightType.TryToParse(Properties.Settings.Default.LightType);
            MVH.MainWindowHelper.TryToParse(Properties.Settings.Default.View3DFlags, vm.View3DFlags);
            MVH.MainWindowHelper.TryToParse(Properties.Settings.Default.View3DOptions, vm.View3DOptions);
            vm.DataSource.TryToParse(Properties.Settings.Default.DataSource);
            vm.MaterialRemoveData.Enable = Properties.Settings.Default.MaterialRemove;
            vm.ProbeSize.TryToParse(Properties.Settings.Default.ProbeSize);
            vm.ProbeColor.TryToParse(Properties.Settings.Default.ProbeColor);
            vm.ProbeShape.TryToParse(Properties.Settings.Default.ProbeShape);
            vm.PanelOuterMaterial.TryToParse(Properties.Settings.Default.PanelOuterMaterial);
            vm.PanelInnerMaterial.TryToParse(Properties.Settings.Default.PanelInnerMaterial);
            vm.PanelFragmentOptions.TryToParse(Properties.Settings.Default.PanelFragmentType);
            vm.SectionDivisionOptions.TryToParse(Properties.Settings.Default.SectionDivision);
            vm.LinkMovementController.Enable = Properties.Settings.Default.DynamicTransition;
            vm.ConnectionManager.IpAddress = Properties.Settings.Default.IpAddress;
            vm.ConnectionManager.SampleTime = Properties.Settings.Default.SampleTime;
        }

    }
}
