using Machine.ViewModels.Base;
using Machine.ViewModels.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MVMIoc = Machine.ViewModels.Ioc;
using CVM = CncViewer.Models;

namespace CncViewer.ConfigEditor.DataSource.File.Xml
{
    public class DataSource
    {
        private IConfigProvider _configProvider;
         
        private ICommand _openCommand;
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new RelayCommand(() => OpenCommandImpl()));

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(() => SaveCommandImpl()));

        public DataSource(IConfigProvider configProvider)
        {
            _configProvider= configProvider;
        }

        private void SaveCommandImpl()
        {
            var dlg = MVMIoc.SimpleIoc<IFileDialog>.GetInstance("SaveFile");

            dlg.AddExtension = true;
            dlg.DefaultExt = "xCncLink";
            dlg.Filter = "Links to variable configuration |*.xCncLink";

            var b = dlg.ShowDialog();

            if (b.HasValue && b.Value)
            {
                var data = _configProvider.ToModel();

                if(data != null) 
                {
                    SaveConfiguration(dlg.FileName, data);
                }
            }
        }

        private void OpenCommandImpl()
        {
            var dlg = MVMIoc.SimpleIoc<IFileDialog>.GetInstance("OpenFile");

            dlg.AddExtension = true;
            dlg.DefaultExt = "xCncLink";
            dlg.Filter = "Links to variable configuration |*.xCncLink";

            var b = dlg.ShowDialog();

            if (b.HasValue && b.Value)
            {
                var data = LoadConfiguration(dlg.FileName);

                if(data != null) 
                {
                    _configProvider.LoadViewModels(data);
                }
            }
        }

        private static void SaveConfiguration(string fileName, CVM.ConnectionData data)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(CVM.ConnectionData));

            using (var writer = new System.IO.StreamWriter(fileName))
            {
                serializer.Serialize(writer, data);
            }
        }

        private static CVM.ConnectionData LoadConfiguration(string fileName)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(CVM.ConnectionData));

            using (var reader = new System.IO.StreamReader(fileName))
            {
                return (CVM.ConnectionData)serializer.Deserialize(reader);
            }
        }
    }
}
