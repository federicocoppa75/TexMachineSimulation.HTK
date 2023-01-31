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

namespace CncViewer.Connection.DataSource.File.Xml
{
    public class DataSource : CVCI.IDataSource
    {
        private ICommand _openCommand;
        public ICommand OpenCommand => _openCommand ?? (_openCommand = new RelayCommand(() => OpenCommandImpl()));

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
                    DeployVariable(data);
                }
            }
        }

        private void DeployVariable(ConnectionData data)
        {
            var variables = MVMIoc.SimpleIoc<CVCI.IConnectionData>.GetInstance().Variables;

            variables.Clear();

            foreach (var link in data.Links) 
            {
                variables.Add(link.ToVariable());
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
