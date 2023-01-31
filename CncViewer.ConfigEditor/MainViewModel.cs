using CncViewer.ConfigEditor.DataSource.File.Xml;
using CncViewer.ConfigEditor.ViewModels.Links;
using Machine.ViewModels.Base;
using Machine.ViewModels.Base.Implementation;
using Machine.ViewModels.Interfaces;
using Machine.ViewModels.Interfaces.MachineElements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVMIoc = Machine.ViewModels.Ioc;
using VMUI = Machine.ViewModels.UI;
using CVCEDSFX = CncViewer.ConfigEditor.DataSource.File.Xml;

namespace CncViewer.ConfigEditor
{
    class MainViewModel : BaseElementsCollectionViewModel, IConfigProvider
    {
        public VMUI.IOptionProvider DataSource => MVMIoc.SimpleIoc<VMUI.IOptionProvider>.GetInstance();

        //public ObservableCollection<LinkViewModel> Links { get; private set; } = new ObservableCollection<LinkViewModel>();
        public IList<LinkViewModel> Links { get; private set; } = new ObservableCollection<LinkViewModel>();

        private LinkViewModel _selectedLink;
        public LinkViewModel SelectedLink
        {
            get => _selectedLink; 
            set => Set(ref _selectedLink, value, nameof(SelectedLink));
        }

        private CVCEDSFX.DataSource _cfgDataSource;
        public CVCEDSFX.DataSource CfgDataSource => _cfgDataSource ?? (_cfgDataSource = new CVCEDSFX.DataSource(this));

        public MainViewModel() : base()
        {
        }

        protected override void AddElement(IEnumerable<IMachineElement> elements)
        {
            foreach (var element in elements) 
            {
                AddElement(element);
            }
        }

        protected override void RemoveElement(IEnumerable<IMachineElement> elements)
        {
            foreach (var element in elements)
            {
                RemoveElement(element);
            }
        }

        protected override void Clear()
        {
            Links.Clear();
        }

        private void AddElement(IMachineElement element) 
        {
            if(element.LinkToParent != null)
            {
                switch(element.LinkToParent.MoveType)
                {
                    case Machine.Data.Enums.LinkMoveType.Pneumatic:
                        Links.Add(new BinaryLinkViewModel(element.LinkToParent.Id) { Description = element.LinkToParent.Description });
                        break;
                    case Machine.Data.Enums.LinkMoveType.Linear:
                        Links.Add(new LinearLinkViewModel(element.LinkToParent.Id) { Description = element.LinkToParent.Description });
                        break;
                    default:
                        throw new InvalidOperationException($"Link type {element.LinkToParent.MoveType} not managed!");
                }
            }

            foreach (var item in element.Children)
            {
                AddElement(item);
            }
        }

        private void RemoveElement(IMachineElement element) 
        {
            foreach (var item in element.Children)
            {
                RemoveElement(item);
            }

            if(element.LinkToParent != null)
            {
                var link = Links.FirstOrDefault(o => o.LinkId == element.LinkToParent.Id);

                if(link != null) Links.Remove(link);
            }            
        }
    }
}
