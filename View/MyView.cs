using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace Wpf_2048
{
    static public partial class MyView
    {
        static public void AddElements(UniformGrid obj)
        {
            for (int i = 0; i < obj.Columns * obj.Rows; i++)
            {

                TextBlock b = new TextBlock()
                {
                    Background = Brushes.LightGray
                    ,Margin = new Thickness(3)
                    ,Text = i.ToString()
                    ,FontSize = 30
                    ,Name = $"myTextBlock{i}"
                };

                Binding binding = new Binding();
                binding.Path = new PropertyPath($"MyCollection[{i}]"); // свойство элемента-источника
                b.SetBinding(TextBlock.TextProperty, binding);
                obj.Children.Add(b);
            }
        }
    }
}

