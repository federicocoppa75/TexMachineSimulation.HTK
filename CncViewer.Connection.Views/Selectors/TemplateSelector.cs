using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace CncViewer.Connection.Views.Selectors
{
    [ContentProperty("Templates")]
    internal abstract class TemplateSelector<T> : DataTemplateSelector
    {
        public List<TemplateSelectorItem<T>> Templates { get; set; } = new List<TemplateSelectorItem<T>>();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dt = null;

            if (TryGetPropertyToCompare(item, out T value))
            {
                foreach (var t in Templates)
                {
                    if (EqualityComparer<T>.Default.Equals(value, t.When))
                    {
                        dt = t.Then;
                        break;
                    }
                }
            }

            return dt;
        }

        protected abstract bool TryGetPropertyToCompare(object item, out T value);
    }

    [ContentProperty("Then")]
    internal class TemplateSelectorItem<T>
    {
        public T When { get; set; }
        public DataTemplate Then { get; set; }

    }
}
