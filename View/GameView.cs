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
    static public partial class GameView
    {
        static public void AddElements(UniformGrid parent)
        {
            for (int i = 0; i < parent.Columns * parent.Rows; i++)
            {
                Border border = new Border()
                {
                    Background = Brushes.LightGray
                    ,Margin = new Thickness(3)
                    ,CornerRadius = new CornerRadius(8)
                };

                TextBlock textBlock = new TextBlock()
                {
                    FontSize = 35
                    ,HorizontalAlignment = HorizontalAlignment.Center
                    ,VerticalAlignment = VerticalAlignment.Center
                };

                Binding binding = new Binding();
                binding.Path = new PropertyPath($"observableCollection[{i}]");
                textBlock.SetBinding(TextBlock.TextProperty, binding);

                border.Child = textBlock;
                parent.Children.Add(border);
            }
        }
    }
}

