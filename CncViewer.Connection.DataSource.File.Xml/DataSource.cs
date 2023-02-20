using Machine.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MVMIoc = Machine.ViewModels.Ioc;
using CVM = CncViewer.Models;
using CncViewer.Models;
using CVCI = CncViewer.Connection.Interfaces;
using Machine.ViewModels.UI;
using MVMUIDSF = Machine.ViewModels.UI.DataSource.File;
using System.IO;
using System.Linq;

namespace CncViewer.Connection.DataSource.File.Xml
{
    public class DataSource : CVCI.IDataSource, MVMUIDSF.IDataSourceExtension
    {
        private string _lastVariableFile;
        private const string _extension = "xCncLink";

        private ICommand _openCommand;
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new RelayCommand(() => OpenCommandImpl()));

        public IEnumerable<string> Files
        {
            get
            {
                var list = new List<string>();

                if (!string.IsNullOrEmpty(_lastVariableFile))
                {
                    list.Add(_lastVariableFile);
                }

                return list;
            }
        }

        public DataSource()
        {
            MVMIoc.SimpleIoc<MVMUIDSF.IDataSourceExtension>.Register(this);
        }

        private void OpenCommandImpl()
        {
            var dlg = MVMIoc.SimpleIoc<IFileDialog>.GetInstance("OpenFile");

            dlg.AddExtension = true;
            dlg.DefaultExt = _extension;
            dlg.Filter = $"Links to variable configuration |*.{_extension}";

            var b = dlg.ShowDialog();

            if (b.HasValue && b.Value)
            {
                LoadVariables(dlg.FileName);
            }
        }

        private void LoadVariables(string fileName)
        {
            var data = LoadConfiguration(fileName);

            if (data != null)
            {
                DeployVariable(data);
            }

            _lastVariableFile = fileName;
        }

        private void DeployVariable(ConnectionData data)
        {
            var variables = MVMIoc.SimpleIoc<CVCI.IConnectionData>.GetInstance().Variables;

            variables.Clear();

            foreach (var link in data.Links) 
            {
                variables.Add(link.ToVariable());
            }

            foreach (var input in data.Inputs)
            {
                variables.Add(input.ToVariable());
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

        public bool OnEnvironmentLoaded(string extractionPath)
        {
            var dir = new DirectoryInfo(extractionPath);

            if(dir.Exists)
            {
                var file = dir.GetFiles($"*.{_extension}").FirstOrDefault();
                
                if(file != null) 
                { 
                    LoadVariables(file.FullName);
                }
            }


            return true;
        }
    }
}
