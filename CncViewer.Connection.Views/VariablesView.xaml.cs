using CncViewer.Connection.Views.ViewModels;
using System;
using System.Collections.Generic;
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

namespace CncViewer.Connection.Views
{
    /// <summary>
    /// Interaction logic for VariablesView.xaml
    /// </summary>
    public partial class VariablesView : UserControl
    {
        public VariablesView()
        {
            InitializeComponent();
            DataContext = new VariablesViewModel();
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
