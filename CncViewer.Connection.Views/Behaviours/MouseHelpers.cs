using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace CncViewer.Connection.Views.Behaviours
{
    internal static class MouseHelpers
    {
        #region MouseLeftButtonDown

        public static ICommand GetMouseLeftButtonDown(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseLeftButtonDownProperty);
        }

        public static void SetMouseLeftButtonDown(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseLeftButtonDownProperty, value);
        }

        public static readonly DependencyProperty MouseLeftButtonDownProperty =
            DependencyProperty.RegisterAttached("MouseLeftButtonDown",
            typeof(ICommand),
            typeof(MouseHelpers),
            new PropertyMetadata(null, new PropertyChangedCallback(MouseLeftButtonDownEnter)));

        private static void MouseLeftButtonDownEnter(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Control;

            if (element != null) element.MouseLeftButtonDown += Element_MouseLeftButtonDown;
        }
        private static void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            var command = GetMouseLeftButtonDown(element);

            if (command != null && command.CanExecute(null))
            {
                command.Execute(null);
            }
        }


        #endregion

        #region MouseLeftButtonUp

        public static ICommand GetMouseLeftButtonUp(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseLeftButtonUpProperty);
        }

        public static void SetMouseLeftButtonUp(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseLeftButtonUpProperty, value);
        }

        public static readonly DependencyProperty MouseLeftButtonUpProperty =
            DependencyProperty.RegisterAttached("MouseLeftButtonUp",
            typeof(ICommand),
            typeof(MouseHelpers),
            new PropertyMetadata(null, new PropertyChangedCallback(MouseLeftButtonUpEnter)));

        private static void MouseLeftButtonUpEnter(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Control;

            if (element != null) element.MouseLeftButtonUp += Element_MouseLeftButtonUp;
        }

        private static void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            var command = GetMouseLeftButtonUp(element);

            if (command != null && command.CanExecute(null))
            {
                command.Execute(null);
            }
        }


        #endregion
    }
}
